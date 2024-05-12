using UnityEngine;

namespace Alter {

    public static class GameColorDomain {

        public static void CombineRenderColor(GameBusinessContext ctx, CellEntity src, CellEntity dst) {
            var oldColor = dst.LogicColor_Get();
            var newColor = src.LogicColor_Get();
            var nextColor = oldColor + newColor;
            dst.SetRenderColor(nextColor);
        }

        public static Color PickRandomColor(GameBusinessContext ctx) {
            var config = ctx.templateInfraContext.Config_Get();
            var colorArr = config.colorArr;
            var colorWeightArr = config.colorWeightArr;

            int totalWeight = 0;
            foreach (var weight in colorWeightArr) {
                totalWeight += weight;
            }

            int randomWeight = ctx.randomService.NextIntRange(0, totalWeight + 1);

            int cumulativeWeight = 0;
            for (int i = 0; i < colorArr.Length; i++) {
                cumulativeWeight += colorWeightArr[i];
                if (randomWeight <= cumulativeWeight) {
                    return colorArr[i];
                }
            }

            return Color.white;
        }

        public static void CombineLogicColor(GameBusinessContext ctx, CellEntity src, CellEntity dst) {
            var oldColor = dst.LogicColor_Get();
            var newColor = src.LogicColor_Get();
            var nextColor = oldColor + newColor;
            dst.SetRenderColor(nextColor);
            dst.SetLogicColor(nextColor);
        }

    }

}