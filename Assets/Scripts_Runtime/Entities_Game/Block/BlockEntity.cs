using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alter {

    public class BlockEntity : MonoBehaviour {

        // Base Info
        public int entityIndex;
        public int typeID;
        public string typeName;

        // Pos
        public Vector2 Pos => transform.position;
        public Vector2Int PosInt => Pos_GetPosInt();
        public Vector2 originalPos;

        // Size
        public Vector2Int sizeInt;

        // Cells
        [SerializeField] Transform cellsRoot;
        List<CellSubEntity> cells;

        public void Ctor() {
            cells = new List<CellSubEntity>();
        }

        // Cells
        public void AddCell(CellSubEntity cell) {
            cells.Add(cell);
            cell.transform.SetParent(cellsRoot);
        }

        public void ForEachCells(Action<CellSubEntity> action) {
            foreach (var cell in cells) {
                action(cell);
            }
        }

        // Pos
        public void Pos_SetPos(Vector2 pos) {
            transform.position = pos;
        }

        Vector2Int Pos_GetPosInt() {
            return transform.position.RoundToVector3Int().ToVector2Int();
        }

        // Move
        public bool Move_CheckConstraint(Vector2 constraintSize, Vector2 constraintCenter, Vector2 pos, Vector2 axis) {
            var min = constraintCenter - constraintSize / 2 + constraintCenter - Vector2.one;
            var max = constraintCenter + constraintSize / 2 + constraintCenter;
            if (pos.x + axis.x >= max.x || pos.x + axis.x <= min.x) {
                return false;
            }
            if (pos.y + axis.y >= max.y || pos.y + axis.y <= min.y) {
                return false;
            }
            return true;
        }

        public void TearDown() {
            Destroy(gameObject);
        }

    }

}