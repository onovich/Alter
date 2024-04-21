using UnityEngine;

namespace Alter {

    public static class GameBlockDomain {

        public static void SpawnBlock(GameBusinessContext ctx, int typeID, Vector2Int pos) {
            var block = GameFactory.Block_Spawn(ctx.idRecordService,
                                                ctx.templateInfraContext,
                                                ctx.assetsInfraContext,
                                                pos);
            ctx.SetCurrentBlock(block);
            block.fsmComponent.Moving_Enter();
        }

        public static void SpawnCellArrFromBlock(GameBusinessContext ctx, int typeID, Vector2Int pos) {
            var has = ctx.templateInfraContext.Block_TryGet(typeID, out var blockTM);
            if (!has) {
                GLog.LogError($"Block {typeID} not found");
                return;
            }
            var map = ctx.currentMapEntity;
            var block = ctx.currentBlock;
            blockTM.ForEachCellsLocalPos((localPos) => {
                var blockPos = pos + localPos;
                var cell = GameCellDomain.Spawn(ctx, blockPos);
                block.AddCell(cell);
            });
        }

        public static void ApplyConstraint(GameBusinessContext ctx) {
            var block = ctx.currentBlock;
            var offset = block.Move_GetConstraintOffset(ctx.currentMapEntity.mapSize, ctx.currentMapEntity.Pos, block.PosInt);
            block.Pos_SetPos(block.PosInt + offset);
        }

        public static void ApplyMove(GameBusinessContext ctx, BlockEntity block, Vector2Int dir) {
            var pos = block.PosInt;
            block.Pos_SetPos(pos + dir);
        }

        public static void ApplyCheckLanding(GameBusinessContext ctx) {
        }

        public static void ApplyFalling(GameBusinessContext ctx, BlockEntity block) {
            if (!ctx.gameEntity.IsFallingFrame) {
                return;
            }
            var dir = Vector2Int.down;
            var pos = block.PosInt;
            block.Pos_SetPos(pos + dir);
        }

        static bool CheckInAir(GameBusinessContext ctx, BlockEntity block) {
            var dir = Vector2Int.down;
            var pos = block.PosInt;
            var map = ctx.currentMapEntity;
            var mapSize = map.mapSize;
            var mapPos = map.Pos;
            return block.Move_CheckInAir(mapSize, mapPos, pos, dir);
        }

        static bool CheckNextIsNotLandingCell(GameBusinessContext ctx, BlockEntity block) {
            return true;
        }

    }

}