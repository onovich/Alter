using UnityEngine;

namespace Alter {

    public class CellSubEntity : MonoBehaviour {

        [SerializeField] SpriteRenderer spr;

        public void Ctor() {

        }

        public void Pos_SetPos(Vector2Int pos) {
            transform.position = pos.ToVector3Int();
        }

        public void TearDown() {
            Destroy(gameObject);
        }

    }

}