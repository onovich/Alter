using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Oshi.Modifier {
    public class SolidEditorElement : MonoBehaviour {

        public int typeID;
        public bool isTrap;
        public bool isBox;

        void OnDrawGizmos() {

            if (isBox) {
                var color = Color.white;
                color.a = 0.5f;

                Gizmos.color = color;
                var size = Vector3.one;

                var style = new GUIStyle() {
                    fontSize = 15,
                    alignment = TextAnchor.MiddleCenter,
                    normal = new GUIStyleState() { textColor = Color.white }
                };

                Gizmos.DrawWireCube(transform.position, size);
                Handles.Label(transform.position, "箱", style);
            }

            if (isTrap) {
                var color = Color.white;
                color.a = 0.5f;

                Gizmos.color = color;
                var size = Vector3.one;

                var style = new GUIStyle() {
                    fontSize = 15,
                    alignment = TextAnchor.MiddleCenter,
                    normal = new GUIStyleState() { textColor = Color.white }
                };

                Gizmos.DrawWireCube(transform.position, size);
                Handles.Label(transform.position, "阱", style);
            }

        }

    }

}