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

    }

}