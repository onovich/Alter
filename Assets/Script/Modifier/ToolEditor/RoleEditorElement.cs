using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oshi.Modifier {
    public class RoleEditorElement : MonoBehaviour {

        public int typeID;

        void OnDrawGizmos() {

            var color = Color.yellow;
            color.a = 0.5f;

            Gizmos.color = color;
            var size = Vector3.one;

            Gizmos.DrawWireCube(transform.position, size);

        }

    }

}