using UnityEngine;

namespace Alter {

    public static class GameCellFSMController {

        public static void TickFSM(GameBusinessContext ctx, CellEntity cell, float dt) {

            TickFSM_Any(ctx, cell, dt);

            CellFSMStatus status = cell.fsmComponent.Status;
            if (status == CellFSMStatus.Moving) {
                TickFSM_Moving(ctx, cell, dt);
            } else if (status == CellFSMStatus.Landing) {
                TickFSM_Landing(ctx, cell, dt);
            } else if (status == CellFSMStatus.Hanging) {
                TickFSM_Hanging(ctx, cell, dt);
            } else if (status == CellFSMStatus.Dying) {
                TickFSM_Dying(ctx, cell, dt);
            } else {
                GLog.LogError($"GameRoleFSMController.TickFSM: unknown status: {status}");
            }

        }

        static void TickFSM_Any(GameBusinessContext ctx, CellEntity cell, float fixdt) {
            cell.RecordLatPos();
        }

        static void TickFSM_Moving(GameBusinessContext ctx, CellEntity cell, float fixdt) {
            CellFSMComponent fsm = cell.fsmComponent;
            if (fsm.moving_isEntering) {
                fsm.moving_isEntering = false;
            }
            var moveDir = ctx.inputEntity.moveAxis;
            GameCellDomain.ApplyMove(ctx, cell, moveDir);
            GameCellDomain.ApplyFalling(ctx, cell);
        }

        static void TickFSM_Landing(GameBusinessContext ctx, CellEntity cell, float fixdt) {
            CellFSMComponent fsm = cell.fsmComponent;
            if (fsm.landing_isEntering) {
                fsm.landing_isEntering = false;
            }
        }

        static void TickFSM_Hanging(GameBusinessContext ctx, CellEntity cell, float fixdt) {
            CellFSMComponent fsm = cell.fsmComponent;
            if (fsm.hanging_isEntering) {
                fsm.hanging_isEntering = false;
            }
        }

        static void TickFSM_Dying(GameBusinessContext ctx, CellEntity cell, float fixdt) {
            CellFSMComponent fsm = cell.fsmComponent;
            if (fsm.dying_isEntering) {
                fsm.dying_isEntering = false;
            }
        }

    }

}