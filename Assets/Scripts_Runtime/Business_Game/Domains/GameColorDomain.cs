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
            var randomIndex = ctx.randomService.NextIntRange(0, colorArr.Length);
            return colorArr[randomIndex];
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