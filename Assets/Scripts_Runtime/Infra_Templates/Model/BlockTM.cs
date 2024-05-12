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
        public UnityEngine.Color meshColor;
        public bool useRandomColor;
        public Material meshMaterial;

        [Header("Block Cells")]
        public BlockShapeTM[] shapeArr;

        public void ForEachCellsLocalPos(int shapeIndex, Action<int, Vector2Int> action) {
            var shape = shapeArr[shapeIndex];
            shape.ForEachCells(action);
        }

    }

}