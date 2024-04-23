using UnityEngine;

namespace Alter {

    public class CellEntity : MonoBehaviour {

        public int entityID;

        // Pos
        public Vector2 Pos => transform.position;
        public Vector2Int PosInt => Pos_GetPosInt();

        // Render
        [SerializeField] SpriteRenderer spr;

        public void Ctor() {
        }

        public void SetSprColor(Color color) {
            spr.color = color;
        }

        public void SetSpr(Sprite sprite) {
            spr.sprite = sprite;
        }

        public void SetSprMaterial(Material material) {
            spr.material = material;
        }

        public void Pos_SetPos(Vector2Int pos) {
            transform.position = pos.ToVector3Int();
        }

        public void Pos_SetLocalPos(Vector2Int localPos) {
            transform.localPosition = localPos.ToVector3Int();
        }

        Vector2Int Pos_GetPosInt() {
            return transform.position.RoundToVector3Int().ToVector2Int();
        }

        public void SetParent(Transform parent) {
            transform.SetParent(parent, true);
        }

        public void TearDown() {
            Destroy(gameObject);
        }

    }

}