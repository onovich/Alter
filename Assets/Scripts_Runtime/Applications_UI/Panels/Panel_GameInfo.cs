using System;
using UnityEngine;
using UnityEngine.UI;
using TenonKit.Loom;
using System.Collections.Generic;

namespace Alter.UI {

    public class Panel_GameInfo : MonoBehaviour, IPanel {

        [SerializeField] Text timeText;
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

        public void OnDestroy() {
            restartBtn.onClick.RemoveAllListeners();
            OnRestartBtnClickHandle = null;
        }

    }

}