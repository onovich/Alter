using System;

namespace Alter {

    public class GameEntity {

        float fallingTimer;
        int currentFallingFrame;
        float fallingInterval;

        bool isFallingFrame;
        public bool IsFallingFrame => isFallingFrame;

        public GameFSMComponent fsmComponent;

        public GameEntity() {
            fsmComponent = new GameFSMComponent();
            currentFallingFrame = 0;
            isFallingFrame = false;
        }

        public void SetFallingInterval(float fallingInterval) {
            this.fallingInterval = fallingInterval;
        }

        public void ApplyFallingInterval(float dt) {
            fallingTimer += dt;
            if (fallingTimer < fallingInterval) {
                return;
            }
            fallingTimer -= fallingInterval;

            currentFallingFrame++;
            isFallingFrame = true;
        }

        public void ResetFallingFrame() {
            currentFallingFrame = 0;
            isFallingFrame = false;
        }

    }

}