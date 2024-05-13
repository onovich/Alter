using System;
using UnityEngine;

namespace Alter {

    public static class GridUtils {

        static Vector2Int[] temp = new Vector2Int[20];

        public static int GetRotateGridsWithoutGC(Vector2Int[] points, Vector2 center, float angleDegrees, out Vector2Int[] rotatedPoints) {
            rotatedPoints = temp;
            float angleRadians = angleDegrees * Mathf.Deg2Rad;
            float cosTheta = Mathf.Cos(angleRadians);
            float sinTheta = Mathf.Sin(angleRadians);
            for (int i = 0; i < points.Length; i++) {
                Vector2 translatedPoint = new Vector2(points[i].x - center.x, points[i].y - center.y);
                Vector2 rotatedTranslatedPoint = new Vector2(
                    translatedPoint.x * cosTheta - translatedPoint.y * sinTheta,
                    translatedPoint.x * sinTheta + translatedPoint.y * cosTheta
                );
                rotatedPoints[i] = new Vector2Int(
                    Mathf.RoundToInt(rotatedTranslatedPoint.x + center.x),
                    Mathf.RoundToInt(rotatedTranslatedPoint.y + center.y)
                );
            }
            return points.Length;
        }

        public static Vector2Int GridIndexToPositionInt(int column, int row, Vector2Int MapSize) {
            var worldX = column - MapSize.x / 2;
            var worldY = row - MapSize.y / 2;
            return new Vector2Int(worldX, worldY);
        }

        public static Vector2Int PositionToGridIndex(Vector2Int pos, Vector2Int MapSize) {
            var column = pos.x + MapSize.x / 2;
            var row = pos.y + MapSize.y / 2;
            return new Vector2Int(column, row);
        }

        public static void ForEachGridBySize(Vector2Int pos, Vector2Int size, Action<Vector2Int> action) {
            for (int x = 0; x < size.x; x++) {
                for (int y = 0; y < size.y; y++) {
                    var grid = new Vector2Int(pos.x + x, pos.y + y);
                    action(grid);
                }
            }
        }

        public static void ForEachBottomGridBySize(Vector2Int pos, Vector2Int size, Action<Vector2Int> action) {
            for (int x = 0; x < size.x; x++) {
                var grid = new Vector2Int(pos.x + x, pos.y);
                action(grid);
            }
        }

    }

}