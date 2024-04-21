using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Alter {

    public class MapEntity : MonoBehaviour {

        public int typeID;
        public Vector2Int mapSize;
        [SerializeField] SpriteRenderer spr;

        public Vector2Int spawnPoint;
        public Vector2Int PosInt => transform.position.RoundToVector2Int();

        public float timer;

        public void Ctor() {
            timer = 0;
        }

        public void Mesh_SetSize(Vector2Int size) {
            spr.size = size;
        }

        public void IncTimer(float dt) {
            timer += dt;
        }

        public void TearDown() {
            Destroy(gameObject);
        }

    }

}