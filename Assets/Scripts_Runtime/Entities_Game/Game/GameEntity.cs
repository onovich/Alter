using System;

namespace Alter {

    public class GameEntity {

        float fallingTimer;
        int currentFallingFrame;
        float fallingInterval;

        public GameFSMComponent fsmComponent;

        public GameEntity() {
            fsmComponent = new GameFSMComponent();
            currentFallingFrame = 0;
        }

        public void SetFallingInterval(float fallingInterval) {
            this.fallingInterval = fallingInterval;
        }

        public void ApplyFallingInterval(float dt, Action onNextFrame) {
            fallingTimer += dt;
            if (fallingTimer < fallingInterval) {
                return;
            }
            fallingTimer -= fallingInterval;

            currentFallingFrame++;
            onNextFrame.Invoke();
        }

    }

}