using System.Diagnostics;
using UnityEngine;

namespace Alter {

    public class BlockFSMComponent {

        BlockFSMStatus status;
        public BlockFSMStatus Status => status;

        public bool moving_isEntering;
        public bool landing_isEntering;

        public void Moving_Enter() {
            Reset();
            status = BlockFSMStatus.Moving;
            moving_isEntering = true;
        }

        public void Landing_Enter() {
            Reset();
            status = BlockFSMStatus.Landing;
            landing_isEntering = true;
        }

        public void Reset() {
            moving_isEntering = false;
            landing_isEntering = false;
        }

    }

}