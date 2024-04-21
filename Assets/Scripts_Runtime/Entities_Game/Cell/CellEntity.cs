using UnityEngine;

namespace Alter {

    public class CellEntity : MonoBehaviour {

        public int entityID;

        // Pos
        public Vector2 Pos => transform.position;
        public Vector2Int PosInt => Pos_GetPosInt();

        public Vector2Int lastPosInt;

        // FSM
        public CellFSMComponent fsmComponent;

        // Render
        [SerializeField] SpriteRenderer spr;

        public void Ctor() {
            fsmComponent = new CellFSMComponent();
        }

        public void SetSprColor(Color color) {
            spr.color = color;
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

        // Move
        public bool Move_CheckInConstraint(Vector2Int constraintSize, Vector2 constraintCenter, Vector2 pos, Vector2Int axis) {
            Vector2Int min = (constraintCenter - constraintSize / 2 + constraintCenter).RoundToVector2Int();
            Vector2Int max = (constraintCenter + constraintSize / 2 + constraintCenter).RoundToVector2Int();
            if (pos.x + axis.x > max.x || pos.x + axis.x <= min.x) {
                return false;
            }
            if (pos.y + axis.y <= min.y) {
                return false;
            }
            return true;
        }

        public void TearDown() {
            Destroy(gameObject);
        }

    }

}