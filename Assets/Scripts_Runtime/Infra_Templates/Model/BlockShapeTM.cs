using System;
using UnityEngine;

namespace Alter {

    [CreateAssetMenu(fileName = "TM_BlockShape", menuName = "Alter/TM/BlockShape")]
    public class BlockShapeTM : ScriptableObject {

        public Vector2Int sizeInt;
        public Vector2Int[] cells;

        public void ForEachCells(Action<int, Vector2Int> action) {
            for (int i = 0; i < cells.Length; i++) {
                action(i, cells[i]);
            }
        }

        public Vector2 GetCenterFloat() {
            return new Vector2((float)sizeInt.x / 2, (float)sizeInt.y / 2);
        }

    }

}