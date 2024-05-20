using System;
using UnityEngine;

namespace Alter {

    public static class GameCellDomain {

        public static CellEntity Spawn(GameBusinessContext ctx, Vector2Int pos) {
            var cell = GameFactory.Cell_Spawn(ctx.idRecordService,
                                              ctx.assetsInfraContext,
                                              pos);

            return cell;
        }

        public static void CombineToRepo(GameBusinessContext ctx, CellEntity cell) {
            if (ctx.cellRepo.TryGetCellByPos(cell.PosInt, out var oldCell)) {
                GameColorDomain.CombineLogicColor(ctx, cell, oldCell);
                cell.TearDown();
            } else {
                ctx.cellRepo.Add(cell, cell.entityID);
                cell.gameObject.name = "Cell_" + cell.entityID;
                SetSortingLayerToCell(ctx, cell);
            }
        }

        public static void SetSortingLayerToCell(GameBusinessContext ctx, CellEntity cell) {
            cell.SetSortingLayer(SortingLayerConst.Cell);
        }

        public static void CheckCellFillRowsAndMark(GameBusinessContext ctx) {
            var rows = ctx.currentMapEntity.mapSize.y;
            for (int y = 0; y < rows; y++) {
                if (CheckCellFillARowWithSameColor(ctx, y)) {
                    MarkCellFillARow(ctx, y);
                    return;
                }
            }
        }

        static void ApplyFallingAboveRow(GameBusinessContext ctx, int clearedRow) {
            var cellRepo = ctx.cellRepo;
            var columns = ctx.currentMapEntity.mapSize.x;
            var rows = ctx.currentMapEntity.mapSize.y;
            var map = ctx.currentMapEntity;
            for (int column = 0; column < columns; column++) {
                for (int row = clearedRow + 1; row < rows; row++) {
                    var pos = GridUtils.GridIndexToPositionInt(column, row, map.mapSize);
                    var has = cellRepo.TryGetCellByPos(pos, out var cell);
                    if (has) {
                        var newPos = GridUtils.GridIndexToPositionInt(column, row - 1, map.mapSize);
                        cell.Pos_SetPos(newPos);
                        cellRepo.UpdatePos(pos, cell);
                    }
                }
            }
        }

        static void MarkCellFillARow(GameBusinessContext ctx, int row) {
            var cellRepo = ctx.cellRepo;
            var columns = ctx.currentMapEntity.mapSize.x;
            var map = ctx.currentMapEntity;
            for (int column = 0; column < columns; column++) {
                var pos = GridUtils.GridIndexToPositionInt(column, row, map.mapSize);
                if (cellRepo.TryGetCellByPos(pos, out var cell)) {
                    ctx.cellRepo.EnqueueClearingTask(cell, row);
                }
            }
            var game = ctx.gameEntity;
            game.fsmComponent.Clearing_Enter();
        }

        static bool CheckCellFillARowWithSameColor(GameBusinessContext ctx, int row) {
            var cellRepo = ctx.cellRepo;
            var columns = ctx.currentMapEntity.mapSize.x;
            var map = ctx.currentMapEntity;
            var first = GridUtils.GridIndexToPositionInt(0, row, map.mapSize);
            var hasFirst = cellRepo.TryGetCellByPos(first, out var firstCell);
            if (!hasFirst) {
                return false;
            }
            var firstColor = firstCell.LogicColor_Get();
            for (int column = 0; column < columns; column++) {
                var pos = GridUtils.GridIndexToPositionInt(column, row, map.mapSize);
                var has = cellRepo.TryGetCellByPos(pos, out var cell)
                && cell.LogicColor_Get() == firstColor;
                if (!has) {
                    return false;
                }
            }
            return true;
        }

        public static void ApplyClearing(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            if (!game.IsClearingFrame) {
                return;
            }

            var cellRepo = ctx.cellRepo;
            var row = cellRepo.GetFirstNotEmptyRow();
            var cell = cellRepo.DequeueClearingTask(row);
            if (row == -1) {
                GLog.LogWarning("row is -1");
                return;
            }
            if (cell == null) {
                GLog.LogWarning("cell is null, row: " + row);
                return;
            }
            UnSpawn(ctx, cell);

            if (cellRepo.GetCountOfClearingTask(row) == 0) {
                var map = ctx.currentMapEntity;
                game.fsmComponent.Gaming_Enter();
                if (map.hasGravity) {
                    ApplyFallingAboveRow(ctx, row);
                }
                GameScoreDomain.AddScore(ctx, cell.logicColor);
            }
        }

        public static void UnSpawn(GameBusinessContext ctx, CellEntity cell) {
            ctx.cellRepo.Remove(cell);
            cell.TearDown();
        }

    }

}