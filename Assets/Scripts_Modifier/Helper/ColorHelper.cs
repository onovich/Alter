#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;
using Alter;
using System.Linq;

namespace Alter.Modifier {

    public static class ColorHelper {

        public static List<Color> GetWebSafeColors() {
            var colors = new List<Color>();
            // int[] steps = { 0, 51, 102, 153, 204, 255 };
            int[] steps = { 0, 85, 170, 255 };

            foreach (int r in steps) {
                foreach (int g in steps) {
                    foreach (int b in steps) {
                        colors.Add(new Color().FromRGB(r, g, b));
                    }
                }
            }
            return colors;
        }

        public static float ColorDistance(Color color1, Color color2) {
            float rDiff = color1.r - color2.r;
            float gDiff = color1.g - color2.g;
            float bDiff = color1.b - color2.b;
            return Mathf.Sqrt(rDiff * rDiff + gDiff * gDiff + bDiff * bDiff);
        }

        public static Color FindClosestWebSafeColor(Color inputColor, List<Color> webSafeColors) {
            return webSafeColors.OrderBy(c => ColorDistance(c, inputColor)).First();
        }

    }

}
#endif