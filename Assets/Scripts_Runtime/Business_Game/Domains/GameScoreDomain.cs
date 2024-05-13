using UnityEngine;

namespace Alter {

    public static class GameScoreDomain {

        public static void AddScore(GameBusinessContext ctx, Color logicColor) {
            var game = ctx.gameEntity;
            var score = ctx.templateInfraContext.Config_GetColorScore(logicColor);
            game.AddScore(score);
        }

        public static int GetNextSoce(GameBusinessContext ctx) {
            var nextBlock = ctx.previewBlock;
            var logicColor = nextBlock.logicColor;
            var score = ctx.templateInfraContext.Config_GetColorScore(logicColor);
            return score;
        }

        public static void ResetScore(GameBusinessContext ctx) {
            var game = ctx.gameEntity;
            game.ResetScore();
        }

    }

}