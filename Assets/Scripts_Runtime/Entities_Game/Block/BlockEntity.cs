using System.Collections.Generic;
using UnityEngine;

namespace Alter {
    public class BlockEntity : MonoBehaviour {

        [SerializeField] Transform cellRoot;

        public int entityID;

        // Pos
        public Vector2 Pos => transform.position;
        public Vector2Int PosInt => Pos_GetPosInt();

        public BoundsInt bounds;

        public Vector2Int lastPosInt;

        // FSM
        public BlockFSMComponent fsmComponent;

        // Cell
        public List<CellEntity> cellList;

        public void Ctor() {
            fsmComponent = new BlockFSMComponent();
            cellList = new List<CellEntity>();
        }

        public void RecordLatPos() {
            lastPosInt = PosInt;
        }

        public void Pos_SetPos(Vector2Int pos) {
            transform.position = pos.ToVector3Int();
        }

        Vector2Int Pos_GetPosInt() {
            return transform.position.RoundToVector3Int().ToVector2Int();
        }

        // Cell
        public void AddCell(CellEntity cell) {
            cell.transform.SetParent(cellRoot, true);
            cellList.Add(cell);
        }

        public bool Cell_IsInBlock(int cellID) {
            return cellList.Exists(cell => cell.entityID == cellID);
        }

        public void Rotate() {
            var _size = new Vector2(bounds.Size.x, bounds.Size.y);
            Vector2Int center = PosInt + (_size / 2).RoundToVector2Int();
            foreach (var cell in cellList) {
                Vector2Int offset = cell.PosInt - center;
                Vector2Int newPos = center + new Vector2Int(offset.y, -offset.x);
                cell.Pos_SetPos(newPos);
            }
            var size = new Vector2Int(bounds.Size.y, bounds.Size.x);
            center = new Vector2Int(center.y, center.x);
            bounds = new BoundsInt(center, size);
        }

        // Move
        public Vector2Int Move_GetConstraintOffset(Vector2Int constraintSize, Vector2Int constraintCenter) {
            Vector2Int blockMin = PosInt;
            Vector2Int blockMax = PosInt + bounds.Size;

            Vector2Int min = constraintCenter - constraintSize / 2 + constraintCenter + Vector2Int.up;
            Vector2Int max = constraintCenter + constraintSize / 2 + constraintCenter;

            Vector2Int offset = Vector2Int.zero;
            if (blockMax.x > max.x) {
                offset.x += max.x - blockMax.x;
            }
            if (blockMin.x <= min.x) {
                offset.x += min.x - blockMin.x;
            }
            if (blockMin.y < min.y) {
                offset.y += min.y - blockMin.y;
            }
            return offset;
        }

        public bool Move_CheckInAir(Vector2Int constraintSize, Vector2Int constraintCenter, Vector2Int pos, Vector2Int axis) {
            Vector2Int min = constraintCenter - constraintSize / 2 + constraintCenter;
            if (pos.y + axis.y <= min.y) {
                return false;
            }
            return true;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.green;
            var center = PosInt + bounds.Size / 2;
            Gizmos.DrawWireCube(center.ToVector3Int(), bounds.Size.ToVector3Int());
        }

        public void TearDown() {
            cellList.Clear();
            Destroy(gameObject);
        }

    }

}