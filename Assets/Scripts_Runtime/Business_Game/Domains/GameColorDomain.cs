using UnityEngine;

namespace Alter {

    public static class GameColorDomain {

        public static Color GetSaveColor(Color src, ColorTM colorTM) {
            return ColorHelper.FindClosestWebSafeColor(src, colorTM.colors);
        }

        public static void CombineRenderColor(GameBusinessContext ctx, CellEntity src, CellEntity dst) {
            var oldColor = dst.LogicColor_Get();
            var newColor = src.LogicColor_Get();
            var nextColor = oldColor + newColor;
            var colorTM = ctx.templateInfraContext.Color_Get();
            var saveColor = GetSaveColor(nextColor, colorTM);
            dst.SetRenderColor(saveColor);
        }

        public static void ResetRenderColor(GameBusinessContext ctx, CellEntity cell) {
            var color = cell.LogicColor_Get();
            var colorTM = ctx.templateInfraContext.Color_Get();
            var saveColor = GetSaveColor(color, colorTM);
            cell.SetRenderColor(saveColor);
        }

        public static Color PickRandomColor(GameConfig config, ColorTM colorTM, RandomService rd) {
            var colorArr = config.colorArr;
            var colorWeightArr = config.colorWeightArr;

            int totalWeight = 0;
            foreach (var weight in colorWeightArr) {
                totalWeight += weight;
            }

            int randomWeight = rd.NextIntRange(0, totalWeight + 1);
            var color = Color.white;
            var saveColor = GetSaveColor(color, colorTM);

            int cumulativeWeight = 0;
            for (int i = 0; i < colorArr.Length; i++) {
                cumulativeWeight += colorWeightArr[i];
                if (randomWeight <= cumulativeWeight) {
                    color = colorArr[i];
                    saveColor = GetSaveColor(color, colorTM);
                    return saveColor;
                }
            }
            return saveColor;
        }

        public static void CombineLogicColor(GameBusinessContext ctx, CellEntity src, CellEntity dst) {
            var oldColor = dst.LogicColor_Get();
            var newColor = src.LogicColor_Get();
            var nextColor = oldColor + newColor;
            var colorTM = ctx.templateInfraContext.Color_Get();
            var saveColor = GetSaveColor(nextColor, colorTM);
            dst.SetRenderColor(saveColor);
            dst.SetLogicColor(saveColor);
        }

    }

}