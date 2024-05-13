using System;

namespace Alter {

    public class GameEntity {

        // Score
        int score;
        public int Score => score;

        // Falling
        float fallingTimer;
        int currentFallingFrame;
        public int CurrentFallingFrame => currentFallingFrame;
        float fallingInterval;
        bool isFallingFrame;
        public bool IsFallingFrame => isFallingFrame;

        // Clearing
        float clearingTimer;
        int currentClearingFrame;
        public int CurrentClearingFrame => currentClearingFrame;
        float clearingInterval;
        bool isClearingFrame;
        public bool IsClearingFrame => isClearingFrame;

        public GameFSMComponent fsmComponent;

        public GameEntity() {
            fsmComponent = new GameFSMComponent();
            currentFallingFrame = 0;
            isFallingFrame = false;
        }

        // Falling
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
            isFallingFrame = false;
        }

        // Clearing
        public void SetClearingInterval(float clearingInterval) {
            this.clearingInterval = clearingInterval;
        }

        public void ApplyClearingInterval(float dt) {
            clearingTimer += dt;
            if (clearingTimer < clearingInterval) {
                return;
            }
            clearingTimer -= clearingInterval;

            currentClearingFrame++;
            isClearingFrame = true;
        }

        public void ResetClearingFrame() {
            isClearingFrame = false;
        }

        // Score
        public void AddScore(int score) {
            this.score += score;
        }

        public void ResetScore() {
            score = 0;
        }

    }

}