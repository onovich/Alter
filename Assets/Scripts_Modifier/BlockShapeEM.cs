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
        public Vector2Int sizeInt;
        public Vector2Int centerInt;

        [Button("Load")]
        void Load() {
            sizeInt = shapeTM.sizeInt;
            GetCells();
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        void BakeCells() {
            shapeTM.cells = new bool[sizeInt.x * sizeInt.y];
            for (int x = 0; x < sizeInt.x; x++) {
                for (int y = 0; y < sizeInt.y; y++) {
                    shapeTM.cells[x + y * sizeInt.x] = cells[x, y];
                }
            }
        }

        [Button("Bake")]
        void Bake() {
            shapeTM.sizeInt = sizeInt;
            shapeTM.centerInt = centerInt;
            BakeCells();
            EditorUtility.SetDirty(shapeTM);
            AssetDatabase.SaveAssets();
        }

        void GetCells() {
            cells = new bool[sizeInt.x, sizeInt.y];
            for (int x = 0; x < sizeInt.x; x++) {
                for (int y = 0; y < sizeInt.y; y++) {
                    cells[x, y] = shapeTM.cells[x + y * sizeInt.x];
                }
            }
        }

        [Button("Clear")]
        void Clear() {
            cells = new bool[sizeInt.x, sizeInt.y];
        }

        [Button("Resize")]
        void Resize() {
            var newCells = new bool[sizeInt.x, sizeInt.y];
            for (int x = 0; x < Mathf.Min(sizeInt.x, cells.GetLength(0)); x++) {
                for (int y = 0; y < Mathf.Min(sizeInt.y, cells.GetLength(1)); y++) {
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