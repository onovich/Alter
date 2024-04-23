using System;
using UnityEngine;

namespace Alter {

    [CreateAssetMenu(fileName = "TM_BlockShape", menuName = "Alter/TM/BlockShape")]
    public class BlockShapeTM : ScriptableObject {

        public Vector2Int centerInt;
        public Vector2Int sizeInt;
        public bool[] cells;

        public Vector2 GetCenterFloat() {
            return new Vector2((float)sizeInt.x / 2, (float)centerInt.y / 2);
        }

        public void ForEachCellsLocalPos(Action<int, Vector2Int> action) {
            for (var x = 0; x < sizeInt.x; x++) {
                for (var y = 0; y < sizeInt.y; y++) {
                    var index = x + y * sizeInt.x;
                    if (cells[index]) {
                        action(index, new Vector2Int(x, sizeInt.y - 1 - y));
                    }
                }
            }
        }

    }

}