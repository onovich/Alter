using System;

namespace Alter {

    public class GameEntity {

        float timer;
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

        public void Tick(float dt, Action onNextFrame) {
            timer += dt;
            if (timer < fallingInterval) {
                return;
            }
            timer -= fallingInterval;

            currentFallingFrame++;
            onNextFrame.Invoke();
        }

    }

}