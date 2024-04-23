using UnityEngine;

namespace Alter {

    public static class GameBlockDomain {

        public static bool CheckNextSpawnable(GameBusinessContext ctx) {
            var map = ctx.currentMapEntity;
            var pos = map.spawnPoint;
            var nextTypeID = ctx.nextBlockTypeID;
            var spawnable = CheckCellArrSpawnable(ctx, ctx.previewBlock, nextTypeID, pos);
            return spawnable;
        }

        public static void SpawnPreviewBlock(GameBusinessContext ctx) {
            var map = ctx.currentMapEntity;
            var pos = map.previewPoint;
            var nextTypeID = ctx.nextBlockTypeID;
            var block = GameFactory.Block_Spawn(ctx.idRecordService,
                                                nextTypeID,
                                                ctx.templateInfraContext,
                                                ctx.assetsInfraContext,
                                                pos);
            ctx.SetPreviewBlock(block);
            block.fsmComponent.None_Enter();
            SpawnCellArrFromBlock(ctx, block, nextTypeID, pos);
        }

        public static void RefreshPreviewBlock(GameBusinessContext ctx) {
            var block = ctx.previewBlock;
            UnSpawnPreview(ctx, block);
            SpawnPreviewBlock(ctx);
        }

        public static void SpawnRandomBlock(GameBusinessContext ctx) {
            var map = ctx.currentMapEntity;
            var pos = map.spawnPoint;
            var nextTypeID = ctx.nextBlockTypeID == -1 ? ctx.templateInfraContext.Block_GetRandom(ctx.randomService).typeID : ctx.nextBlockTypeID;
            SpawnBlock(ctx, nextTypeID, pos);
        }

        static void SpawnBlock(GameBusinessContext ctx, int typeID, Vector2Int pos) {
            var block = GameFactory.Block_Spawn(ctx.idRecordService,
                                                typeID,
                                                ctx.templateInfraContext,
                                                ctx.assetsInfraContext,
                                                pos);
            ctx.SetCurrentBlock(block);
            block.fsmComponent.Moving_Enter();

            SpawnCellArrFromBlock(ctx, block, typeID, pos);

            // Record Next Block Type ID
            var nextBlockTM = ctx.templateInfraContext.Block_GetRandom(ctx.randomService);
            ctx.nextBlockTypeID = nextBlockTM.typeID;
        }

        public static bool CheckCellArrSpawnable(GameBusinessContext ctx, BlockEntity block, int typeID, Vector2Int pos) {
            var has = ctx.templateInfraContext.Block_TryGet(typeID, out var blockTM);
            if (!has) {
                GLog.LogError($"Block {typeID} not found");
            }
            var map = ctx.currentMapEntity;
            var index = block.currentIndex;
            var spawnable = true;
            blockTM.ForEachCellsLocalPos(index, (cellIndex, localPos) => {
                var cellPos = pos + localPos;
                var hasCell = ctx.cellRepo.TryGetCellByPos(cellPos, out var cell);
                spawnable &= !hasCell;
                if(!spawnable) {
                    return;
                }
            });
            return spawnable;
        }

        public static void SpawnCellArrFromBlock(GameBusinessContext ctx, BlockEntity block, int typeID, Vector2Int pos) {
            var has = ctx.templateInfraContext.Block_TryGet(typeID, out var blockTM);
            if (!has) {
                GLog.LogError($"Block {typeID} not found");
                return;
            }
            var map = ctx.currentMapEntity;
            var index = block.currentIndex;
            blockTM.ForEachCellsLocalPos(index, (cellIndex, localPos) => {
                var cellPos = pos + localPos;
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
            var size = block.SizeInt;
            var hasCell = false;
            block.cellSlotComponent.ForEach((index, cell) => {
                var cellPos = cell.PosInt;
                var next = cellPos + Vector2Int.down;
                var hasNext = ctx.cellRepo.TryGetCellByPos(next, out var nextCell);
                hasCell |= hasNext;
                if (hasCell) {
                    return;
                }
            });

            return !hasCell;
        }

        public static void UnSpawnCurrent(GameBusinessContext ctx, BlockEntity cell) {
            ctx.SetCurrentBlock(null);
            cell.TearDown();
        }

        public static void UnSpawnPreview(GameBusinessContext ctx, BlockEntity cell) {
            ctx.SetPreviewBlock(null);
            cell.TearDown();
        }

    }

}