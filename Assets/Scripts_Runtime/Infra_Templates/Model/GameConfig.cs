using System;
using MortiseFrame.Swing;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Alter {

    [CreateAssetMenu(fileName = "SO_GameConfig", menuName = "Alter/GameConfig")]
    public class GameConfig : ScriptableObject {

        // Game
        [Header("Game Config")]
        public float gameResetEnterTime;
        public float gameTotalTime;
        public int originalMapTypeID;
        public float gameFallingIntervalTime;
        public float gameInputIntervalTime;

        // Color
        [Header("Cell Color")]
        public Color[] colorArr;
        public int[] colorWeightArr;
        public int[] colorScoreArr;
        public ColorTM colorTM;

        [Button("Fix Color")]
        public void FixColor() {
            if (colorArr == null || colorArr.Length == 0) {
                return;
            }
            for (int i = 0; i < colorArr.Length; i++) {
                colorArr[i] = ColorHelper.FindClosestWebSafeColor(colorArr[i], colorTM.colors);
            }
            Debug.Log("Fix Color Complete: " + colorArr.Length + " colors");
        }

        // Camera
        [Header("Shake Config")]
        public float cameraShakeFrequency_roleDamage;
        public float cameraShakeAmplitude_roleDamage;
        public float cameraShakeDuration_roleDamage;
        public EasingType cameraShakeEasingType_roleDamage;
        public EasingMode cameraShakeEasingMode_roleDamage;

    }

}