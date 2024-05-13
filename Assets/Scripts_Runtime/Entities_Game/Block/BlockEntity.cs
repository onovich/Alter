using System.Collections.Generic;
using UnityEngine;

namespace Alter {
    public class BlockEntity : MonoBehaviour {

        [SerializeField] Transform cellRoot;

        public int entityID;
        public string typeName;
        public int typeID;

        // Pos
        public Vector2 Pos => transform.position;
        public Vector2Int PosInt => Pos_GetPosInt();

        public Vector2Int lastPosInt;

        // FSM
        public BlockFSMComponent fsmComponent;

        // Cell
        public BlockCellSlotComponent cellSlotComponent;
        public BlockShapeComponent shapeComponent;
        public Vector2Int SizeInt => GetSizeInt();

        public void Ctor() {
            fsmComponent = new BlockFSMComponent();
            cellSlotComponent = new BlockCellSlotComponent();
            shapeComponent = new BlockShapeComponent();
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
            cellSlotComponent.Add(cell);
        }

        public void Rotate() {
            var next = shapeComponent.TurnToNextShape();
            var len = cellSlotComponent.TakeAll(out var cells);
            for (int i = 0; i < len; i++) {
                var cell = cells[i];
                var index = i;
                cell.Pos_SetLocalPos(shapeComponent.shape[index]);
            }
        }

        public Vector2Int GetSizeInt() {
            return shapeComponent.sizeInt;
        }

        // Move
        public Vector2Int Move_GetConstraintOffset(Vector2Int constraintSize, Vector2Int constraintCenter) {
            var shape = shapeComponent;
            var maxOffset = Vector2Int.zero;
            var minOffset = Vector2Int.zero;
            shape.ForEachCell(cell => {
                var offset = GetConstraintOffset(constraintSize, constraintCenter, PosInt + cell);
                maxOffset = Vector2Int.Max(maxOffset, offset);
                minOffset = Vector2Int.Min(minOffset, offset);
            });
            if (maxOffset == Vector2Int.zero) {
                return minOffset;
            }
            return maxOffset;
        }

        Vector2Int GetConstraintOffset(Vector2Int constraintSize, Vector2Int constraintCenter, Vector2Int pos) {
            Vector2Int min = constraintCenter - constraintSize / 2 + constraintCenter;
            Vector2Int max = constraintCenter + constraintSize / 2 + constraintCenter - Vector2Int.one;

            Vector2Int offset = Vector2Int.zero;
            if (pos.x > max.x) {
                offset.x += max.x - pos.x;
            }
            if (pos.x <= min.x) {
                offset.x += min.x - pos.x;
            }
            if (pos.y < min.y) {
                offset.y += min.y - pos.y;
            }
            return offset;
        }

        public bool Move_CheckInAir(Vector2Int constraintSize, Vector2Int constraintCenter, Vector2Int axis) {
            var shape = shapeComponent;
            var inAir = true;
            shape.ForEachCell(cell => {
                var pos = PosInt + cell + axis;
                inAir &= CheckInAir(constraintSize, constraintCenter, pos);
            });
            return inAir;
        }

        bool CheckInAir(Vector2Int constraintSize, Vector2Int constraintCenter, Vector2Int pos) {
            Vector2Int min = constraintCenter - constraintSize / 2 + constraintCenter;
            if (pos.y < min.y) {
                return false;
            }
            return true;
        }

        public void TearDown() {
            fsmComponent.Reset();
            cellSlotComponent.Clear();
            Destroy(gameObject);
        }

    }

}