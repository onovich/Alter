using UnityEngine;

namespace Alter {

    public class BlockFSMComponent {

        BlockFSMStatus status;
        public BlockFSMStatus Status => status;

        public bool moving_isEntering;
        public bool landing_isEntering;
        public bool forceLanding_isEntering;

        public void None_Enter() {
            Reset();
            status = BlockFSMStatus.None;
        }

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

        public void ForceLanding_Enter() {
            Reset();
            status = BlockFSMStatus.ForceLanding;
            forceLanding_isEntering = true;
        }

        public void Reset() {
            moving_isEntering = false;
            landing_isEntering = false;
            forceLanding_isEntering = false;
        }

    }

}