using System;
using UnityEngine;

namespace Alter {

    public class BlockEntity : MonoBehaviour {

        // Base Info
        public int entityIndex;
        public int typeID;
        public string typeName;

        // Render
        [SerializeField] SpriteRenderer spr;

        // Pos
        public Vector2 Pos => transform.position;
        public Vector2Int PosInt => Pos_GetPosInt();
        public Vector2 originalPos;

        // Size
        public Vector2Int sizeInt;

        public void Ctor() {
        }

        // Pos
        public void Pos_SetPos(Vector2 pos) {
            transform.position = pos;
        }

        Vector2Int Pos_GetPosInt() {
            return transform.position.RoundToVector3Int().ToVector2Int();
        }

        // Push
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

        // Size
        public void Size_SetSize(Vector2Int size) {
            spr.size = size;
            this.sizeInt = size;
        }

        // Mesh
        public void Mesh_Set(Sprite sp) {
            this.spr.sprite = sp;
        }

        public void Mesh_SetMaterial(Material mat) {
            this.spr.material = mat;
        }

        public void TearDown() {
            Destroy(gameObject);
        }

    }

}