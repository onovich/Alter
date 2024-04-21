using UnityEngine;

namespace Alter {

    public struct BoundsInt {

        Vector2Int min;
        public Vector2Int Min => min;

        Vector2Int max;
        public Vector2Int Max => max;

        public BoundsInt(Vector2Int center, Vector2Int size) {
            min = center - size / 2;
            max = center + size / 2;
        }

        public Vector2Int Center => GetCenter();
        public Vector2Int Size => GetSize();

        public bool Contains(Vector2Int point) {
            return point.x >= Min.x && point.x <= Max.x && point.y >= Min.y && point.y <= Max.y;
        }

        Vector2Int GetCenter() {
            return (Min + Max) / 2;
        }

        Vector2Int GetSize() {
            return Max - Min;
        }

        float GetHeight() {
            return Max.y - Min.y;
        }

        float GetWidth() {
            return Max.x - Min.x;
        }

        public void SetCenter(Vector2Int center) {
            var size = GetSize();
            min = center - size / 2;
            max = center + size / 2;
        }

    }

}