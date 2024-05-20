using UnityEngine;

namespace Alter {

    public static class ColorExtension {

        public static Color FromARGB(this Color color, int alpha, int red, int green, int blue) {
            color.a = alpha / 255f;
            color.r = red / 255f;
            color.g = green / 255f;
            color.b = blue / 255f;
            return color;
        }

        public static Color FromRGB(this Color color, int red, int green, int blue) {
            return color.FromARGB(255, red, green, blue);
        }

    }

}