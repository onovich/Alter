using UnityEngine;

namespace Alter {

    public static class GameCellFSMController {

        public static void FixedTickFSM(GameBusinessContext ctx, CellEntity cell, float dt) {

            FixedTickFSM_Any(ctx, cell, dt);

            CellFSMStatus status = cell.fsmComponent.Status;
            if (status == CellFSMStatus.Moving) {
                FixedTickFSM_Moving(ctx, cell, dt);
            } else if (status == CellFSMStatus.Landing) {
                FixedTickFSM_Landing(ctx, cell, dt);
            } else if (status == CellFSMStatus.Hanging) {
                FixedTickFSM_Hanging(ctx, cell, dt);
            } else if (status == CellFSMStatus.Dying) {
                FixedTickFSM_Dying(ctx, cell, dt);
            } else {
                GLog.LogError($"GameRoleFSMController.FixedTickFSM: unknown status: {status}");
            }

        }

        static void FixedTickFSM_Any(GameBusinessContext ctx, CellEntity cell, float fixdt) {

        }

        static void FixedTickFSM_Moving(GameBusinessContext ctx, CellEntity cell, float fixdt) {
            CellFSMComponent fsm = cell.fsmComponent;
            if (fsm.moving_isEntering) {
                fsm.moving_isEntering = false;
            }

            GameCellDomain.ApplyFalling(ctx, cell);
        }

        static void FixedTickFSM_Landing(GameBusinessContext ctx, CellEntity cell, float fixdt) {
            CellFSMComponent fsm = cell.fsmComponent;
            if (fsm.landing_isEntering) {
                fsm.landing_isEntering = false;
            }
        }

        static void FixedTickFSM_Hanging(GameBusinessContext ctx, CellEntity cell, float fixdt) {
            CellFSMComponent fsm = cell.fsmComponent;
            if (fsm.hanging_isEntering) {
                fsm.hanging_isEntering = false;
            }
        }

        static void FixedTickFSM_Dying(GameBusinessContext ctx, CellEntity cell, float fixdt) {
            CellFSMComponent fsm = cell.fsmComponent;
            if (fsm.dying_isEntering) {
                fsm.dying_isEntering = false;
            }
        }

    }

}