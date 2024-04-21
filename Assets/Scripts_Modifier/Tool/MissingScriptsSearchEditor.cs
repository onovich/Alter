using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MissingScriptsSearchEditor : EditorWindow {
    [MenuItem("Tools/MissingScriptsSearchEditor")]
    public static void ShowWindow() {
        GetWindow(typeof(MissingScriptsSearchEditor));
    }

    public void OnGUI() {
        if (GUILayout.Button("Find Missing Scripts in Scene")) {
            FindInScene();
        }
    }

    private static void FindInScene() {
        GameObject[] go = GetAllObjectsInScene();
        List<GameObject> missing = new List<GameObject>();
        foreach (GameObject g in go) {
            Component[] components = g.GetComponents<Component>();
            foreach (Component c in components) {
                if (c == null) {
                    Debug.LogError("Missing Component in GameObject: " + FullPath(g), g);
                    missing.Add(g);
                }
            }
        }
        if (missing.Count == 0) {
            Debug.Log("No missing scripts found in scene.");
        }
    }

    private static GameObject[] GetAllObjectsInScene() {
        List<GameObject> objectsInScene = new List<GameObject>();

        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]) {
            if (go.hideFlags == HideFlags.None) {
                if (go.transform.parent == null) {
                    objectsInScene.Add(go);
                }
            }
        }
        return objectsInScene.ToArray();
    }

    private static string FullPath(GameObject go) {
        return go.transform.parent == null ? go.name : FullPath(go.transform.parent.gameObject) + "/" + go.name;
    }
}