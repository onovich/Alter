using System;
using System.Drawing;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Alter {

    public class BlockShapeEM : SerializedMonoBehaviour {

        [Header("Bake Target")]
        public BlockShapeTM shapeTM;

        [Header("Block Cells")]
        public Vector2Int size;

        [Header("Block Info")]
        public int typeID;
        public string typeName;

        [Button("Load")]
        void Load() {
            size = shapeTM.size;
            GetCells();
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        void BakeCells() {
            shapeTM.cells = new bool[size.x * size.y];
            for (int x = 0; x < size.x; x++) {
                for (int y = 0; y < size.y; y++) {
                    shapeTM.cells[x + y * size.x] = cells[x, y];
                }
            }
        }

        [Button("Bake")]
        void Bake() {
            shapeTM.typeID = typeID;
            shapeTM.typeName = typeName;
            shapeTM.size = size;
            BakeCells();
            EditorUtility.SetDirty(shapeTM);
            AssetDatabase.SaveAssets();
        }

        void GetCells() {
            cells = new bool[size.x, size.y];
            for (int x = 0; x < size.x; x++) {
                for (int y = 0; y < size.y; y++) {
                    cells[x, y] = shapeTM.cells[x + y * size.x];
                }
            }
        }

        [Button("Clear")]
        void Clear() {
            cells = new bool[size.x, size.y];
        }

        [Button("Resize")]
        void Resize() {
            var newCells = new bool[size.x, size.y];
            for (int x = 0; x < Mathf.Min(size.x, cells.GetLength(0)); x++) {
                for (int y = 0; y < Mathf.Min(size.y, cells.GetLength(1)); y++) {
                    newCells[x, y] = cells[x, y];
                }
            }
            cells = newCells;
        }

        [TableMatrix(DrawElementMethod = "DrawCell", SquareCells = true)]
        public bool[,] cells;

        bool DrawCell(Rect rect, bool value) {
            if (Event.current.type == EventType.MouseDown &&
            rect.Contains(Event.current.mousePosition)) {
                value = !value;
                GUI.changed = true;
                Event.current.Use();
            }
            EditorGUI.DrawRect(
                rect.Padding(1),
                value ? new UnityEngine.Color(0.1f, 0.8f, 0.2f) :
                new UnityEngine.Color(0, 0, 0, 0.5f));
            return value;
        }
    }

}