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
        public BlockShapeTM shape;

        public void ForEachCellsLocalPos(Action<Vector2Int> action) {
            shape.ForEachCells(action);
        }

    }

}