using System;
using UnityEngine;

namespace Alter {

    public static class GridUtils {

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