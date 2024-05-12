using System;
using MortiseFrame.Swing;
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

        // Camera
        [Header("Shake Config")]
        public float cameraShakeFrequency_roleDamage;
        public float cameraShakeAmplitude_roleDamage;
        public float cameraShakeDuration_roleDamage;
        public EasingType cameraShakeEasingType_roleDamage;
        public EasingMode cameraShakeEasingMode_roleDamage;

    }

}