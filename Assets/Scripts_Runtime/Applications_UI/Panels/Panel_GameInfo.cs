using System;
using UnityEngine;
using UnityEngine.UI;
using TenonKit.Loom;
using System.Collections.Generic;

namespace Alter.UI {

    public class Panel_GameInfo : MonoBehaviour, IPanel {

        [SerializeField] Text timeText;
        [SerializeField] Text stepText;
        [SerializeField] Text scoreText;
        [SerializeField] Text nextScoreText;
        [SerializeField] Button restartBtn;

        public Action OnRestartBtnClickHandle;

        public void Ctor() {
            restartBtn.onClick.AddListener(() => {
                OnRestartBtnClickHandle?.Invoke();
            });
        }

        public void RefreshTime(float time) {
            timeText.text = time.ToString("F0");
        }

        public void RefreshStep(int step) {
            stepText.text = step.ToString();
        }

        public void RefreshScore(int score) {
            scoreText.text = score.ToString();
        }

        public void RefreshNextScore(int score, Color color) {
            nextScoreText.text = score.ToString();
            nextScoreText.color = color;
        }

        public void OnDestroy() {
            restartBtn.onClick.RemoveAllListeners();
            OnRestartBtnClickHandle = null;
        }

    }

}