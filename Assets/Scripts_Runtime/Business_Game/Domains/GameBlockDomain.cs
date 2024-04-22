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
                var cellPos = pos + localPos;
                Debug.Log($"Spawn Cell {cellPos} = {localPos} + {pos}");
                var cell = GameCellDomain.Spawn(ctx, cellPos);
                block.AddCell(cell);
            });
        }

        public static void ApplyConstraint(GameBusinessContext ctx) {
            var block = ctx.currentBlock;
            var offset = block.Move_GetConstraintOffset(ctx.currentMapEntity.mapSize, ctx.currentMapEntity.PosInt);
            block.Pos_SetPos(block.PosInt + offset);
        }

        public static void ApplyMove(GameBusinessContext ctx, BlockEntity block, Vector2Int dir) {
            var pos = block.PosInt;
            block.Pos_SetPos(pos + dir);
        }

        public static void ApplyRotate(GameBusinessContext ctx) {
            if (ctx.inputEntity.isRotate == false) {
                return;
            }
            var block = ctx.currentBlock;
            block.Rotate();
        }

        public static void ApplyCheckLanding(GameBusinessContext ctx) {
            var block = ctx.currentBlock;
            if (CheckInAir(ctx, block) && CheckNextIsNoCell(ctx, block)) {
                return;
            }
            block.fsmComponent.Landing_Enter();
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
            var mapPos = map.PosInt;
            return block.Move_CheckInAir(mapSize, mapPos, pos, dir);
        }

        static bool CheckNextIsNoCell(GameBusinessContext ctx, BlockEntity block) {
            var pos = block.PosInt;
            var size = block.bounds.Size;
            pos += Vector2Int.down;
            var noCell = true;
            GridUtils.ForEachBottomGridBySize(pos, size, (grid) => {
                var has = ctx.cellRepo.TryGetCellByPos(grid, out var cell);
                noCell &= (!has) ||
                          (has && !block.Cell_IsInBlock(cell.entityID));
                if (!noCell) {
                    return;
                }
            });

            return noCell;
        }

        public static void UnSpawn(GameBusinessContext ctx, BlockEntity cell) {
            ctx.SetCurrentBlock(null);
            cell.TearDown();
        }

    }

}