using UnityEngine;

namespace Alter {

    public class CellEntity : MonoBehaviour {

        public int index;

        // Pos
        public Vector2 Pos => transform.position;
        public Vector2Int PosInt => Pos_GetPosInt();

        // Render
        [SerializeField] SpriteRenderer spr;

        // Color
        public Color logicColor;

        public void Ctor() {
        }

        public void SetRenderColor(Color color) {
            spr.color = color;
        }

        public void SetLogicColor(Color color) {
            this.logicColor = color;
        }

        public void SetSortingLayer(string layer) {
            this.spr.sortingLayerName = layer;
        }

        public Color LogicColor_Get() {
            return logicColor;
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