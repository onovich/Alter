using UnityEngine;

namespace Alter {

    public static class GameBusiness {

        public static void Init(GameBusinessContext ctx) {

        }

        public static void StartGame(GameBusinessContext ctx) {
            GameGameDomain.NewGame(ctx);
        }

        public static void ExitGame(GameBusinessContext ctx) {
            GameGameDomain.ExitGame(ctx);
        }

        public static void Tick(GameBusinessContext ctx, float dt) {
            InputEntity inputEntity = ctx.inputEntity;

            ProcessInput(ctx, dt);
            PreTick(ctx, dt);

            const float intervalTime = 0.01f;
            ref float restSec = ref ctx.fixedRestSec;
            restSec += dt;
            if (restSec < intervalTime) {
                FixedTick(ctx, restSec);
                restSec = 0;
            } else {
                while (restSec >= intervalTime) {
                    restSec -= intervalTime;
                    FixedTick(ctx, intervalTime);
                }
            }
            LateTick(ctx, dt);
            inputEntity.Reset();
        }

        public static void ProcessInput(GameBusinessContext ctx, float dt) {
            GameInputDomain.Player_BakeInput(ctx, dt);

            var game = ctx.gameEntity;
            var status = game.fsmComponent.status;
        }

        static void PreTick(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            var status = game.fsmComponent.status;
            var map = ctx.currentMapEntity;
            if (status == GameStatus.Gaming) {

                GameGameDomain.ApplyFallingFrame(ctx, dt);

                // Cell In Block
                var block = ctx.currentBlock;
                GameBlockFSMController.TickFSM(ctx, block, dt);

                GameGameDomain.ResetFallingFrame(ctx);

                // Cell In Board
                GameCellDomain.CheckCellFillRowsAndMark(ctx);

                // Result
                GameGameDomain.ApplyGameResult(ctx);
                return;
            }
            if (status == GameStatus.Clearing) {
                GameGameDomain.ApplyClearingFrame(ctx, dt);
                GameCellDomain.ApplyClearing(ctx, dt);
                GameGameDomain.ResetClearingFrame(ctx);
                return;
            }
            if (status == GameStatus.GameOver) {
                GameGameDomain.ApplyGameOver(ctx, dt);
                return;
            }
        }

        static void FixedTick(GameBusinessContext ctx, float fixdt) {
            var game = ctx.gameEntity;
            var status = game.fsmComponent.status;
            if (status == GameStatus.Gaming) {
                Physics2D.Simulate(fixdt);
            }
        }

        static void LateTick(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            var input = ctx.inputEntity;
            var status = game.fsmComponent.status;
            if (status == GameStatus.Gaming) {

                // UI
                UIApp.GameInfo_RefreshTime(ctx.uiContext, game.CurrentFallingFrame);
                UIApp.GameInfo_RefreshStep(ctx.uiContext, input.inputStep);
                UIApp.GameInfo_RefreshScore(ctx.uiContext, game.Score);

            }
            // VFX
            VFXApp.LateTick(ctx.vfxContext, dt);
        }

        public static void TearDown(GameBusinessContext ctx) {
            var game = ctx.gameEntity;
            var status = game.fsmComponent.status;
            if (status == GameStatus.Gaming) {
                ExitGame(ctx);
            }
        }

        public static void OnDrawGizmos(GameBusinessContext ctx, bool drawBlockSizeGizmos) {
            if (ctx == null) {
                return;
            }
            var game = ctx.gameEntity;
            var status = game.fsmComponent.status;
            if (status == GameStatus.Gaming) {
                if (drawBlockSizeGizmos) {
                    GameBlockDomain.OnDrawGizmos(ctx);
                }
            }
        }

        // UI
        public static void UIGameInfo_OnRestartBtnClick(GameBusinessContext ctx) {
            GameGameDomain.RestartGame(ctx);
        }

        public static void UIGameOver_OnRestartGame(GameBusinessContext ctx) {
            UIApp.GameOver_Close(ctx.uiContext);
            GameGameDomain.RestartGame(ctx);
        }

        public static void UIGameOver_OnExitGameClick(GameBusinessContext ctx) {
            ExitGame(ctx);
            Application.Quit();
            GLog.Log("Application.Quit");
        }

    }

}