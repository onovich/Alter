using System;
using UnityEngine;

namespace Alter {

    [CreateAssetMenu(fileName = "TM_BlockShape", menuName = "Alter/TM/BlockShape")]
    public class BlockShapeTM : ScriptableObject {

        public Vector2Int sizeInt;
        public Vector2Int[] cells;
        public Vector2 centerFloat;

        public void ForEachCells(Action<Vector2Int> action) {
            for (int i = 0; i < cells.Length; i++) {
                action(cells[i]);
            }
        }

    }

}