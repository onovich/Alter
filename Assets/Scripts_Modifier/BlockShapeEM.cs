#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Drawing;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Alter.Modifier {

    public class BlockShapeEM : SerializedMonoBehaviour {

        [Header("Bake Target")]
        public BlockShapeTM shapeTM;

        [Header("Block Cells")]
        public Vector2Int sizeInt;

        [Button("Load")]
        void Load() {
            sizeInt = shapeTM.sizeInt;
            GetCells();
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        void BakeCells() {
            List<Vector2Int> cellList = new List<Vector2Int>();
            for (int x = 0; x < sizeInt.x; x++) {
                for (int y = 0; y < sizeInt.y; y++) {
                    if (cells[x, y] > 0) {
                        cellList.Add(new Vector2Int(x, sizeInt.y - 1 - y));
                    }
                }
            }
            shapeTM.cells = cellList.ToArray();
        }

        [Button("Bake")]
        void Bake() {
            shapeTM.sizeInt = sizeInt;
            BakeCells();
            EditorUtility.SetDirty(shapeTM);
            AssetDatabase.SaveAssets();
        }

        void GetCells() {
            cells = new int[sizeInt.x, sizeInt.y];
            for (int i = 0; i < shapeTM.cells.Length; i++) {
                var x = shapeTM.cells[i].x;
                var y = sizeInt.y - 1 - shapeTM.cells[i].y;
                cells[x, y] = i + 1;
            }
        }

        [Button("Clear")]
        void Clear() {
            cells = new int[sizeInt.x, sizeInt.y];
        }

        [Button("Resize")]
        void Resize() {
            var newCells = new int[sizeInt.x, sizeInt.y];
            for (int x = 0; x < Mathf.Min(sizeInt.x, cells.GetLength(0)); x++) {
                for (int y = 0; y < Mathf.Min(sizeInt.y, cells.GetLength(1)); y++) {
                    newCells[x, y] = cells[x, y];
                }
            }
            cells = newCells;
        }

        [TableMatrix(DrawElementMethod = "DrawCell", SquareCells = true)]
        public int[,] cells;
        int DrawCell(Rect rect, int value) {
            Event e = Event.current;
            if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition)) {
                if (e.button == 0) {  // 鼠标左键
                    value = FixNextIndex(value + 1);
                    value = Math.Min(value, shapeTM.cells.Length);
                } else if (e.button == 1) {  // 鼠标右键
                    value = FixLastIndex(value - 1);
                    value = Math.Max(0, value);
                }
                GUI.changed = true;
                e.Use();
            }
            EditorGUI.DrawRect(
                rect.Padding(1),
                value > 0 ? UnityEngine.Color.grey :
                UnityEngine.Color.black);
            EditorGUI.LabelField(rect, value.ToString());
            return value;
        }

        int FixNextIndex(int value) {
            if (value == 0) return 0;
            for (int i = 0; i < cells.GetLength(0); i++) {
                for (int j = 0; j < cells.GetLength(1); j++) {
                    if (cells[i, j] == value) {
                        return ++value;
                    }
                }
            }
            return 0;
        }

        int FixLastIndex(int value) {
            if (value == 0) return 0;
            for (int i = 0; i < cells.GetLength(0); i++) {
                for (int j = 0; j < cells.GetLength(1); j++) {
                    if (cells[i, j] == value) {
                        return --value;
                    }
                }
            }
            return 0;
        }
    }

}
#endif