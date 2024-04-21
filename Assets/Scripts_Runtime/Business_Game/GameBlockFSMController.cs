using UnityEngine;

namespace Alter {

    public static class GameBlockFSMController {

        public static void TickFSM(GameBusinessContext ctx, BlockEntity block, float dt) {

            TickFSM_Any(ctx, block, dt);

            BlockFSMStatus status = block.fsmComponent.Status;
            if (status == BlockFSMStatus.Moving) {
                TickFSM_Moving(ctx, block, dt);
            } else if (status == BlockFSMStatus.Landing) {
                TickFSM_Landing(ctx, block, dt);
            } else {
                GLog.LogError($"GameRoleFSMController.TickFSM: unknown status: {status}");
            }

        }

        static void TickFSM_Any(GameBusinessContext ctx, BlockEntity block, float fixdt) {
            block.RecordLatPos();
        }

        static void TickFSM_Moving(GameBusinessContext ctx, BlockEntity block, float fixdt) {
            BlockFSMComponent fsm = block.fsmComponent;
            if (fsm.moving_isEntering) {
                fsm.moving_isEntering = false;
            }
            var moveDir = ctx.inputEntity.moveAxis;
            GameBlockDomain.ApplyMove(ctx, block, moveDir);
            GameBlockDomain.ApplyRotate(ctx);
            // GameBlockDomain.ApplyFalling(ctx, block);
            GameBlockDomain.ApplyCheckLanding(ctx);
            GameBlockDomain.ApplyConstraint(ctx);
        }

        static void TickFSM_Landing(GameBusinessContext ctx, BlockEntity block, float fixdt) {
            BlockFSMComponent fsm = block.fsmComponent;
            if (fsm.landing_isEntering) {
                fsm.landing_isEntering = false;
            }
        }

    }

}