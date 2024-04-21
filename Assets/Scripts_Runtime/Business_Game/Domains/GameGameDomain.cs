using System;
using UnityEngine;

namespace Alter {

    public static class GameGameDomain {

        public static void NewGame(GameBusinessContext ctx) {

            var config = ctx.templateInfraContext.Config_Get();

            // Input
            var input = ctx.inputEntity;
            input.SetInputInterval(config.gameInputIntervalTime);

            // Game
            var game = ctx.gameEntity;
            game.SetFallingInterval(config.gameFallingIntervalTime);
            game.fsmComponent.Gaming_Enter(config.gameTotalTime);

            // Map
            var mapTypeID = config.originalMapTypeID;
            var map = GameMapDomain.Spawn(ctx, mapTypeID);
            var has = ctx.templateInfraContext.Map_TryGet(mapTypeID, out var mapTM);
            if (!has) {
                GLog.LogError($"MapTM Not Found {mapTypeID}");
            }

            // Block
            // 临时代码
            var blockTypeID = 1;
            var spawnPoint = mapTM.spawnPoint;
            GameBlockDomain.SpawnBlock(ctx, blockTypeID, spawnPoint);
            GameBlockDomain.SpawnCellArrFromBlock(ctx, blockTypeID, spawnPoint);

            // UI
            UIApp.GameInfo_Open(ctx.uiContext);

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

        public static void ApplyFallingFrame(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            game.ApplyFallingInterval(dt);
        }

        public static void ResetFallingFrame(GameBusinessContext ctx) {
            var game = ctx.gameEntity;
            game.ResetFallingFrame();
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
                GameCellDomain.UnSpawn(ctx, cell);
            }

            // Block
            var block = ctx.currentBlock;
            if (block != null) {
                GameBlockDomain.UnSpawn(ctx, block);
            }

            // Repo
            ctx.Reset();

            // UI
            UIApp.GameOver_Close(ctx.uiContext);
            UIApp.GameInfo_Close(ctx.uiContext);

        }

    }
}