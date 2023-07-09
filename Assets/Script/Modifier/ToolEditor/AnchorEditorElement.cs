using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
                color.a = .5f;
                Gizmos.color = color;

                var style = new GUIStyle() {
                    fontSize = 15,
                    alignment = TextAnchor.MiddleCenter,
                    normal = new GUIStyleState() { textColor = Color.white }
                };

                Gizmos.DrawCube(transform.position, size);
                Handles.Label(transform.position, "目", style);
            }

            if (isEntrance) {
                var color = Color.white;
                color.a = .5f;
                Gizmos.color = color;

                var style = new GUIStyle() {
                    fontSize = 15,
                    alignment = TextAnchor.MiddleCenter,
                    normal = new GUIStyleState() { textColor = Color.white }
                };

                Gizmos.DrawCube(transform.position, size);
                Handles.Label(transform.position, "入", style);
            }

            if (isExport) {
                var color = Color.green;
                color.a = .5f;
                Gizmos.color = color;

                var style = new GUIStyle() {
                    fontSize = 15,
                    alignment = TextAnchor.MiddleCenter,
                    normal = new GUIStyleState() { textColor = Color.white }
                };

                Gizmos.DrawCube(transform.position, size);
                Handles.Label(transform.position, "离", style);
            }

        }

    }

}