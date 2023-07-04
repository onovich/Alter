using UnityEngine;

namespace Oshi.Generic {

    public static class VectorExtension {

        public static Vector3 ToVector3(this Vector2 v) {
            return new Vector3(v.x, v.y, 0);
        }

        public static Vector3 ToVector3(this Vector2Int v) {
            return new Vector3((float)v.x, (float)v.y, 0f);
        }

        public static Vector2 ToVector2(this Vector3 v) {
            return new Vector3(v.x, v.y);
        }

        public static Vector2Int ToVector2Int(this Vector3 v) {
            return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
        }

    }

}