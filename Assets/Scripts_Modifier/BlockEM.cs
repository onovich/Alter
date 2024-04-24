#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Drawing;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Alter {

    public class BlockEM : SerializedMonoBehaviour {

        [Header("Bake Target")]
        public BlockTM blockTM;

        [Header("Block Info")]
        public int typeID;
        public string typeName;

        [Header("Block Mesh")]
        public Sprite mesh;
        public UnityEngine.Color color;
        public Material meshMaterial;

        [Header("Block Shapes")]
        public BlockShapeTM[] shapes;

        [Button("Load")]
        void Load() {
            typeID = blockTM.typeID;
            typeName = blockTM.typeName;
            mesh = blockTM.mesh;
            color = blockTM.meshColor;
            meshMaterial = blockTM.meshMaterial;
            GetShapes();
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        void GetShapes() {
            shapes = new BlockShapeTM[blockTM.shapeArr.Length];
            for (int i = 0; i < blockTM.shapeArr.Length; i++) {
                var shape = blockTM.shapeArr[i];
                shapes[i] = shape;
            }
        }

        void BakeShapes() {
            var shapeList = new List<BlockShapeTM>();
            for (int i = 0; i < shapes.Length; i++) {
                var shape = shapes[i];
                shapeList.Add(shape);
            }
            blockTM.shapeArr = shapeList.ToArray();
        }

        [Button("Bake")]
        void Bake() {
            blockTM.typeID = typeID;
            blockTM.typeName = typeName;
            blockTM.mesh = mesh;
            blockTM.meshColor = color;
            blockTM.meshMaterial = meshMaterial;
            BakeShapes();
            EditorUtility.SetDirty(blockTM);
            AssetDatabase.SaveAssets();
        }

    }

}
#endif