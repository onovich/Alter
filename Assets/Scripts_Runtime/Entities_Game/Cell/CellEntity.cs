using UnityEngine;

namespace Alter {

    public class CellEntity : MonoBehaviour {

        public int entityID;

        // Pos
        public Vector2 Pos => transform.position;
        public Vector2Int PosInt => Pos_GetPosInt();

        public Vector2Int lastPosInt;

        // Render
        [SerializeField] SpriteRenderer spr;

        public void Ctor() {
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

        public void TearDown() {
            Destroy(gameObject);
        }

    }

}