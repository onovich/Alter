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

        public static void SpawnBlockFromPreview(GameBusinessContext ctx) {
            var map = ctx.currentMapEntity;
            var pos = map.spawnPoint;
            var previewBlock = ctx.previewBlock;
            var nextTypeID = previewBlock.typeID;
            var block = SpawnBlock(ctx, nextTypeID, pos);
            var len = previewBlock.cellSlotComponent.TakeAll(out var previewCells);
            for (int i = 0; i < len; i++) {
                var previewCell = previewBlock.cellSlotComponent.Get(i);
                var cell = block.cellSlotComponent.Get(i);
                if (previewCell == null) {
                    continue;
                }
                cell.SetRenderColor(previewCell.LogicColor_Get());
                cell.SetLogicColor(previewCell.LogicColor_Get());
            }
        }

        public static void SpawnRandomBlock(GameBusinessContext ctx) {
            var map = ctx.currentMapEntity;
            var pos = map.spawnPoint;
            var nextTypeID = ctx.nextBlockTypeID == -1 ? ctx.templateInfraContext.Block_GetRandom(ctx.randomService).typeID : ctx.nextBlockTypeID;
            SpawnBlock(ctx, nextTypeID, pos);
        }

        static BlockEntity SpawnBlock(GameBusinessContext ctx, int typeID, Vector2Int pos) {
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
            return block;
        }

        public static bool CheckCellArrSpawnable(GameBusinessContext ctx, BlockEntity block, int typeID, Vector2Int pos) {
            var has = ctx.templateInfraContext.Block_TryGet(typeID, out var blockTM);
            if (!has) {
                GLog.LogError($"Block {typeID} not found");
            }
            var map = ctx.currentMapEntity;
            var spawnable = true;
            blockTM.ForEachCellsLocalPos((cellIndex, localPos) => {
                var cellPos = pos + localPos;
                var hasCell = ctx.cellRepo.TryGetCellByPos(cellPos, out var cell);
                spawnable &= !hasCell;
                if (!spawnable) {
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
            has = ctx.templateInfraContext.Block_TryGet(typeID, out blockTM);
            if (!has) {
                GLog.LogError($"Block {typeID} not found");
                return;
            }
            blockTM.ForEachCellsLocalPos((cellIndex, localPos) => {
                var cellPos = pos + localPos;
                var cell = GameCellDomain.Spawn(ctx, cellPos, cellIndex);
                block.AddCell(cell);
                cell.SetSpr(blockTM.mesh);
                var color = blockTM.useRandomColor ? GameColorDomain.PickRandomColor(ctx) : blockTM.meshColor;
                cell.SetRenderColor(color);
                cell.SetLogicColor(color);
                cell.SetSprMaterial(blockTM.meshMaterial);
                cell.SetSortingLayer(SortingLayerConst.Block);
            });
        }

        public static void ApplyConstraint(GameBusinessContext ctx) {
            var block = ctx.currentBlock;
            var offset = block.Move_GetConstraintOffset(ctx.currentMapEntity.mapSize, ctx.currentMapEntity.PosInt);
            block.Pos_SetPos(block.PosInt + offset);
        }

        public static void ApplyMove(GameBusinessContext ctx, BlockEntity block, Vector2Int dir) {
            var pos = block.PosInt;
            var allow = CheckNextIsNoCellInSameColorOrWhite(ctx, block, dir);
            if (!allow) {
                return;
            }
            block.Pos_SetPos(pos + dir);
        }

        public static void ApplyRotate(GameBusinessContext ctx) {
            if (ctx.inputEntity.isRotate == false) {
                return;
            }
            var block = ctx.currentBlock;
            var allow = CheckNextShapeIsNoCell(ctx, block);
            if (!allow) {
                return;
            }
            block.Rotate();
        }

        public static void ApplyCheckLanding(GameBusinessContext ctx) {
            var block = ctx.currentBlock;
            if (CheckInAir(ctx, block) && CheckNextIsNoCellInSameColorOrWhite(ctx, block, Vector2Int.down)) {
                block.fsmComponent.Moving_Enter();
                return;
            }
            block.fsmComponent.Landing_Enter();
        }

        public static void ApplyFalling(GameBusinessContext ctx, BlockEntity block) {
            if (!ctx.gameEntity.IsFallingFrame) {
                return;
            }
            var allow = CheckNextIsNoCellInSameColorOrWhite(ctx, block, Vector2Int.down);
            if (!allow) {
                return;
            }
            var dir = Vector2Int.down;
            var pos = block.PosInt;
            block.Pos_SetPos(pos + dir);

            // Color
            var len = block.cellSlotComponent.TakeAll(out var cells);
            for (int i = 0; i < len; i++) {
                var cell = cells[i];
                var cellPos = cell.PosInt;
                var has = ctx.cellRepo.TryGetCellByPos(cellPos, out var oldCell);
                if (!has) {
                    continue;
                }
                GameColorDomain.CombineRenderColor(ctx, oldCell, cell);
            }
        }

        static bool CheckInAir(GameBusinessContext ctx, BlockEntity block) {
            var dir = Vector2Int.down;
            var pos = block.PosInt;
            var map = ctx.currentMapEntity;
            var mapSize = map.mapSize;
            var mapPos = map.PosInt;
            return block.Move_CheckInAir(mapSize, mapPos, pos, dir);
        }

        static bool CheckNextShapeIsNoCell(GameBusinessContext ctx, BlockEntity block) {
            var len = block.shapeComponent.shape.TakeNextShape(out var nextShape);
            var hasCell = false;
            for (int i = 0; i < len; i++) {
                var cellPos = nextShape[i];
                var next = block.PosInt + cellPos;
                var hasNext = ctx.cellRepo.TryGetCellByPos(next, out var nextCell);
                hasCell |= hasNext;
                if (hasNext) {
                    return false;
                }
            }
            return !hasCell;
        }

        static bool CheckNextIsNoCellInSameColorOrWhite(GameBusinessContext ctx, BlockEntity block, Vector2Int dir) {
            var hasSameColorCell = false;
            var hasWhiteCell = false;
            var len = block.cellSlotComponent.TakeAll(out var cells);
            for (int i = 0; i < len; i++) {
                var cell = cells[i];
                var cellPos = cell.PosInt;
                var next = cellPos + dir;
                var hasNext = ctx.cellRepo.TryGetCellByPos(next, out var nextCell);
                if (hasNext) {
                    hasSameColorCell |= hasNext & nextCell.LogicColor_Get() == cell.LogicColor_Get();
                    hasWhiteCell |= hasNext & nextCell.LogicColor_Get() == Color.white;
                } else {
                    hasSameColorCell = false;
                }
                if (hasSameColorCell) {
                    return false;
                }
                if (hasWhiteCell) {
                    return false;
                }
            }
            return !hasSameColorCell;
        }

        public static void UnSpawnCurrent(GameBusinessContext ctx, BlockEntity block) {
            ctx.SetCurrentBlock(null);
            block.TearDown();
        }

        public static void UnSpawnPreview(GameBusinessContext ctx, BlockEntity cell) {
            ctx.SetPreviewBlock(null);
            cell.TearDown();
        }

    }

}