using System;

namespace Alter {

    public class GameEntity {

        float timer;
        int currentFrame;
        float frameInterval;

        public GameFSMComponent fsmComponent;

        public GameEntity() {
            fsmComponent = new GameFSMComponent();
            currentFrame = 0;
        }

        public void SetFrameInterval(float interval) {
            frameInterval = interval;
        }

        public void Tick(float dt, Action onNextFrame) {
            timer += dt;
            if (timer < frameInterval) {
                return;
            }
            timer -= frameInterval;

            currentFrame++;
            onNextFrame.Invoke();
        }

    }

}