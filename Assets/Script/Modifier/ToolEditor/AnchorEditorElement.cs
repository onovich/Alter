using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oshi.Modifier {
    public class AnchorEditorElement : MonoBehaviour {

        public int typeID;

        public bool isTarget;
        public bool isEntrance;
        public bool isExport;
        public Vector3Int exportPoint;

        void OnDrawGizmos() {

            var size = Vector3.one;

            if (isTarget) {
                var color = Color.red;
                color.a = 0.5f;
                Gizmos.DrawCube(transform.position, size);
            }

            if (isEntrance) {
                var color = Color.white;
                color.a = 0.5f;
                Gizmos.DrawCube(transform.position, size);
            }

            if (isExport) {
                var color = Color.green;
                color.a = 0.5f;
                Gizmos.DrawCube(transform.position, size);
            }

        }

    }

}