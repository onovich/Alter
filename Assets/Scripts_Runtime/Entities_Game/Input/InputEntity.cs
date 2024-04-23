using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Alter {

    public class InputEntity {

        public Vector2Int moveAxis;
        public bool isRotate;

        public int inputStep;

        InputKeybindingComponent keybindingCom;

        public void Ctor() {
            keybindingCom.Ctor();
        }

        public void ResetMoveAxis() {
            moveAxis = Vector2Int.zero;
        }

        public void ProcessInput(Camera camera, float dt) {

            if (keybindingCom.IsKeyDown(InputKeyEnum.MoveLeft)) {
                moveAxis.x = -1;
                inputStep++;
            }
            if (keybindingCom.IsKeyDown(InputKeyEnum.MoveRight)) {
                moveAxis.x = 1;
                inputStep++;
            }
            if (keybindingCom.IsKeyDown(InputKeyEnum.MoveUp)) {
                isRotate = true;
                inputStep++;
            }
            if (keybindingCom.IsKeyPressing(InputKeyEnum.MoveDown)) {
                moveAxis.y = -1;
                inputStep++;
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
            isRotate = false;
        }

    }

}