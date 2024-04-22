using System;
using UnityEngine;

namespace Alter {

    [CreateAssetMenu(fileName = "TM_BlockShape", menuName = "Alter/TM/BlockShape")]
    public class BlockShapeTM : ScriptableObject {

        public int typeID;
        public string typeName;
        public Vector2Int size;
        public bool[] cells;

        public void ForEachCellsLocalPos(Action<Vector2Int> action) {
            for (var x = 0; x < size.x; x++) {
                for (var y = 0; y < size.y; y++) {
                    var index = x + y * size.x;
                    if (cells[index]) {
                        action(new Vector2Int(x, size.y - 1 - y));
                    }
                }
            }
        }

    }

}