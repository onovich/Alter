using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Alter {

    public class InputEntity {

        public Vector2Int moveAxis;

        float inputTimer;
        float inputInterval;

        InputKeybindingComponent keybindingCom;

        public void Ctor() {
            keybindingCom.Ctor();
        }

        public void SetInputInterval(float inputInterval) {
            this.inputInterval = inputInterval;
        }

        public void ResetMoveAxis() {
            moveAxis = Vector2Int.zero;
        }

        public void ProcessInput(Camera camera, float dt) {

            inputTimer += dt;
            if (inputTimer < inputInterval) {
                return;
            }
            inputTimer -= inputInterval;

            if (keybindingCom.IsKeyPressing(InputKeyEnum.MoveLeft)) {
                moveAxis.x = -1;
            }
            if (keybindingCom.IsKeyPressing(InputKeyEnum.MoveRight)) {
                moveAxis.x = 1;
            }
            if (keybindingCom.IsKeyPressing(InputKeyEnum.MoveUp)) {
                moveAxis.y = 1;
            }
            if (keybindingCom.IsKeyPressing(InputKeyEnum.MoveDown)) {
                moveAxis.y = -1;
            }
            if (moveAxis.x != 0 && moveAxis.y != 0) {
                moveAxis.y = 0;
            }
        }

        public void Keybinding_Set(InputKeyEnum key, KeyCode[] keyCodes) {
            keybindingCom.Bind(key, keyCodes);
        }

        public void Reset() {
            moveAxis = Vector2Int.zero;
        }

    }

}