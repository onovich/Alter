using System;
using UnityEngine;

namespace Alter {

    public static class GameGameDomain {

        public static void NewGame(GameBusinessContext ctx) {

            var config = ctx.templateInfraContext.Config_Get();

            // Game
            var game = ctx.gameEntity;
            game.SetFallingInterval(config.gameFallingIntervalTime);
            game.fsmComponent.Gaming_Enter();

            // Map
            var mapTypeID = config.originalMapTypeID;
            var _ = GameMapDomain.Spawn(ctx, mapTypeID);
            var has = ctx.templateInfraContext.Map_TryGet(mapTypeID, out var mapTM);
            if (!has) {
                GLog.LogError($"MapTM Not Found {mapTypeID}");
            }

            // Block
            // First Block
            ctx.nextBlockTypeID = ctx.templateInfraContext.Block_GetRandom(ctx.randomService).typeID;
            GameBlockDomain.SpawnRandomBlock(ctx);

            // UI
            UIApp.GameInfo_Open(ctx.uiContext);

            // Preview Block
            GameBlockDomain.SpawnPreviewBlock(ctx);

        }

        public static void ApplyGameOver(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            var fsm = game.fsmComponent;

            fsm.GameOver_DecTimer(dt);

            var enterTime = fsm.gameOver_enterTime;
            if (enterTime <= 0) {
                UIApp.GameOver_Open(ctx.uiContext, fsm.gameOver_result);
            }
        }

        // Falling
        public static void ApplyFallingFrame(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            game.ApplyFallingInterval(dt);
        }

        public static void ResetFallingFrame(GameBusinessContext ctx) {
            var game = ctx.gameEntity;
            game.ResetFallingFrame();
        }

        // Clearing
        public static void ApplyClearingFrame(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            game.ApplyClearingInterval(dt);
        }

        public static void ResetClearingFrame(GameBusinessContext ctx) {
            var game = ctx.gameEntity;
            game.ResetClearingFrame();
        }

        public static void RestartGame(GameBusinessContext ctx) {
            var game = ctx.gameEntity;
            var fsm = game.fsmComponent;
            ExitGame(ctx);
            NewGame(ctx);
        }

        public static void ApplyGameResult(GameBusinessContext ctx) {
            var game = ctx.gameEntity;
            var config = ctx.templateInfraContext.Config_Get();
        }

        public static void ApplyGameStage(GameBusinessContext ctx) {
            var stageEnd = CheckCurrentBlockIsLanding(ctx);
            if (stageEnd) {
                NewTurn(ctx);
            }
        }

        public static void ForceNewTurn(GameBusinessContext ctx) {
            NewTurn(ctx);
        }

        static void NewTurn(GameBusinessContext ctx) {
            var block = ctx.currentBlock;
            var len = block.cellSlotComponent.TakeAll(out var cells);
            for (int i = 0; i < len; i++) {
                var cell = cells[i];
                GameCellDomain.CombineToRepo(ctx, cell);
                cell.SetParent(ctx.cellBoard);
            }
            GameBlockDomain.UnSpawnCurrent(ctx, block);

            var game = ctx.gameEntity;
            var config = ctx.templateInfraContext.Config_Get();
            var spawnable = GameBlockDomain.CheckNextSpawnable(ctx);
            if (!spawnable) {
                game.fsmComponent.GameOver_Enter(config.gameResetEnterTime, GameResult.Lose);
                return;
            }

            GameBlockDomain.SpawnBlockFromPreview(ctx);
            GameBlockDomain.RefreshPreviewBlock(ctx);
        }

        static bool CheckCurrentBlockIsLanding(GameBusinessContext ctx) {
            var block = ctx.currentBlock;
            var status = block.fsmComponent.Status;
            if (status == BlockFSMStatus.Landing) {
                return true;
            }
            return false;
        }

        public static void ExitGame(GameBusinessContext ctx) {
            // Game
            var game = ctx.gameEntity;
            game.fsmComponent.NotInGame_Enter();

            // Map
            GameMapDomain.UnSpawn(ctx);

            // Cell
            int cellLen = ctx.cellRepo.TakeAll(out var cellArr);
            for (int i = 0; i < cellLen; i++) {
                var cell = cellArr[i];
                if (cell != null) {
                    GameCellDomain.UnSpawn(ctx, cell);
                }
            }

            // Block
            var block = ctx.currentBlock;
            if (block != null) {
                GameBlockDomain.UnSpawnCurrent(ctx, block);
            }
            var previewBlock = ctx.previewBlock;
            if (previewBlock != null) {
                GameBlockDomain.UnSpawnPreview(ctx, previewBlock);
            }

            // Repo
            ctx.Reset();

            // UI
            UIApp.GameOver_Close(ctx.uiContext);
            UIApp.GameInfo_Close(ctx.uiContext);

        }

    }
}