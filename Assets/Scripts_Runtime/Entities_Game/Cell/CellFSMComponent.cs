using System.Diagnostics;
using UnityEngine;

namespace Alter {

    public class CellFSMComponent {

        CellFSMStatus status;
        public CellFSMStatus Status => status;

        public bool moving_isEntering;
        public bool landing_isEntering;
        public bool hanging_isEntering;
        public bool dying_isEntering;

        public void Moving_Enter() {
            Reset();
            status = CellFSMStatus.Moving;
            moving_isEntering = true;
        }

        public void Landing_Enter() {
            Reset();
            status = CellFSMStatus.Landing;
            landing_isEntering = true;
        }

        public void Hanging_Enter(float gameTime) {
            Reset();
            status = CellFSMStatus.Hanging;
            hanging_isEntering = true;
        }

        public void Dying_Enter() {
            Reset();
            status = CellFSMStatus.Dying;
            dying_isEntering = true;
        }

        public void Reset() {
            moving_isEntering = false;
            landing_isEntering = false;
            hanging_isEntering = false;
            dying_isEntering = false;
        }

    }

}