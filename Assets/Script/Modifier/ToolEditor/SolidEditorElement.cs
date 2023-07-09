using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oshi.Modifier {
    public class SolidEditorElement : MonoBehaviour {

        public int typeID;
        public bool isTrap;
        public bool isBox;

        void OnDrawGizmos() {

            var color = Color.white;
            color.a = 0.5f;

            Gizmos.color = color;
            var size = Vector3.one;

            Gizmos.DrawWireCube(transform.position, size);

        }

    }

}