using System.Collections.Generic;
using UnityEngine;

namespace Alter {
    public class BlockEntity : MonoBehaviour {

        [SerializeField] Transform cellRoot;

        public int entityID;

        // Pos
        public Vector2 Pos => transform.position;
        public Vector2Int PosInt => Pos_GetPosInt();

        public Vector2Int lastPosInt;

        // FSM
        public BlockFSMComponent fsmComponent;

        // Cell
        public BlockCellSlotComponent cellSlotComponent;
        public BlockShapeComponent shapeComponent;
        public int currentIndex;
        public Vector2Int SizeInt => GetSizeInt();

        public void Ctor() {
            fsmComponent = new BlockFSMComponent();
            cellSlotComponent = new BlockCellSlotComponent();
            shapeComponent = new BlockShapeComponent();
            currentIndex = 0;
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

        public bool Cell_IsInBlock(int cellID) {
            return cellSlotComponent.TryGetByEntityID(cellID, out _);
        }

        public void Rotate() {
            currentIndex = (currentIndex + 1) % 4;
            var shape = shapeComponent.Get(currentIndex);
            cellSlotComponent.ForEach((index, cell) => {
                cell.Pos_SetLocalPos(shape.shape[index]);
            });
        }

        public Vector2Int GetSizeInt() {
            var shape = shapeComponent.Get(currentIndex);
            return shape.sizeInt;
        }

        // Move
        public Vector2Int Move_GetConstraintOffset(Vector2Int constraintSize, Vector2Int constraintCenter) {
            var shape = shapeComponent.Get(currentIndex);

            Vector2Int blockMin = PosInt;
            Vector2Int blockMax = PosInt + shape.sizeInt;

            Vector2Int min = constraintCenter - constraintSize / 2 + constraintCenter;
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
            Vector2Int min = constraintCenter - constraintSize / 2 + constraintCenter + Vector2Int.down;
            if (pos.y + axis.y <= min.y) {
                return false;
            }
            return true;
        }

        private void OnDrawGizmos() {
            var shape = shapeComponent.Get(currentIndex);
            Gizmos.color = Color.green;
            var center = Pos + shape.centerFloat;
            var size = shape.sizeInt;
            Gizmos.DrawWireCube(center, new Vector3(size.x, size.y, 0));
            Gizmos.color = Color.red;
            Gizmos.DrawCube(center, new Vector3(.2f, .2f, 0));
            Gizmos.color = Color.white;
            Gizmos.DrawCube(Pos, new Vector3(.2f, .2f, 0));
        }

        public void TearDown() {
            fsmComponent.Reset();
            shapeComponent.Clear();
            cellSlotComponent.Clear();
            Destroy(gameObject);
        }

    }

}