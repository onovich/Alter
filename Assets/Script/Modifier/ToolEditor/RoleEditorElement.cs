using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Oshi.Modifier {
    public class RoleEditorElement : MonoBehaviour {

        public int typeID;

        void OnDrawGizmos() {

            var color = Color.yellow;
            color.a = 0.5f;

            Gizmos.color = color;
            var size = Vector3.one;

            var style = new GUIStyle() {
                fontSize = 15,
                alignment = TextAnchor.MiddleCenter,
                normal = new GUIStyleState() { textColor = Color.white }
            };

            Gizmos.DrawWireCube(transform.position, size);
            Handles.Label(transform.position, "人", style);

        }

    }

}