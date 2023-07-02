using System;
using Oshi.Generic;
using UnityEngine;

namespace Oshi.Infra {

    public class EventCore {

        // Map
        public event Action<Vector2> OnMoveStartHandle;
        public void MoveStart(Vector2 value) {
            if (OnMoveStartHandle != null) {
                OnMoveStartHandle(value);
            } else {
                OshiLog.Warning("OnMoveStartHandle is null");
            }
        }

        public event Action OnMoveEndHandle;
        public void MoveEnd() {
            if (OnMoveEndHandle != null) {
                OnMoveEndHandle.Invoke();
            } else {
                OshiLog.Warning("OnMoveEndHandle is null");
            }
        }

        // Login
        public event Action OnLoginHandle;
        public void Login() {
            if (OnLoginHandle != null) {
                OnLoginHandle.Invoke();
            } else {
                OshiLog.Warning("OnLoginHandle is null");
            }
        }

        // Game Result

        // Setting

        public void ClearAll() {
            OnMoveStartHandle = null;
            OnMoveEndHandle = null;
            OnLoginHandle = null;
        }

    }

}