using System;
using UnityEngine;

namespace Alter {

    [CreateAssetMenu(fileName = "TM_Block", menuName = "Alter/TM/Block")]
    public class BlockTM : ScriptableObject {

        [Header("Block Info")]
        public int typeID;
        public string typeName;

        [Header("Block Mesh")]
        public Sprite mesh;
        public Material meshMaterial;

        [Header("Block Cells")]
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