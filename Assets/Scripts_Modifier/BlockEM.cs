#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Drawing;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Alter.Modifier {

    public class BlockEM : SerializedMonoBehaviour {

        [Header("Bake Target")]
        public BlockTM blockTM;

        [Header("Block Info")]
        public int typeID;
        public string typeName;

        [Header("Block Mesh")]
        public Sprite mesh;
        public Material meshMaterial;

        [Header("Block Shapes")]
        public BlockShapeTM shape;

        [Button("Load")]
        void Load() {
            typeID = blockTM.typeID;
            typeName = blockTM.typeName;
            mesh = blockTM.mesh;
            meshMaterial = blockTM.meshMaterial;
            GetShapes();
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        void GetShapes() {
            shape = blockTM.shape;
        }

        void BakeShapes() {
            blockTM.shape = shape;
        }

        [Button("Bake")]
        void Bake() {
            blockTM.typeID = typeID;
            blockTM.typeName = typeName;
            blockTM.mesh = mesh;
            blockTM.meshMaterial = meshMaterial;
            BakeShapes();
            AddressableHelper.SetAddressable(blockTM, "TM_Block", "TM_Block", true);
            EditorUtility.SetDirty(blockTM);
            AssetDatabase.SaveAssets();
        }

    }

}
#endif