#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Alter.Modifier {

    public class ColorEM : MonoBehaviour {

        [Header("Color TM")]
        public ColorTM colorTM;

        [Header("Test Color")]
        public Color testColor;
        public Color closestColor;

        [Button("Bake")]
        public void Bake() {
            var src = ColorHelper.GetWebSafeColors();
            colorTM.colors = new List<Color>();
            for (int i = 0; i < src.Count; i++) {
                colorTM.colors.Add(src[i]);
            }
            EditorUtility.SetDirty(colorTM);
            AssetDatabase.SaveAssets();
            var colorSize = sizeof(float) * 4 * src.Count;
            var kb = colorSize / 1024f;
            Debug.Log($"Bake ColorTM Complete: {src.Count} colors; size = {colorSize} bytes = {kb} KB");
            src.Clear();
        }

        [Button("Clear")]
        public void Clear() {
            colorTM.colors = null;
            EditorUtility.SetDirty(colorTM);
            AssetDatabase.SaveAssets();
            Debug.Log("Clear ColorTM Complete");
        }

        [Button("Test")]
        public void Test() {
            closestColor = ColorHelper.FindClosestWebSafeColor(testColor, colorTM.colors);
            AssetDatabase.SaveAssets();
        }

    }

}
#endif