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

        public static void AppToRepo(GameBusinessContext ctx, CellEntity cell) {
            ctx.cellRepo.Add(cell);
        }

        public static void CheckCellFillRowsAndMark(GameBusinessContext ctx) {
            var rows = ctx.currentMapEntity.mapSize.y;
            for (int y = 0; y < rows; y++) {
                if (CheckCellFillARow(ctx, y)) {
                    MarkCellFillARow(ctx, y);
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
                    cell.SetSprColor(Color.red);
                    ctx.cellRepo.EnqueueClearingTask(cell);
                }
            }
            var game = ctx.gameEntity;
            game.fsmComponent.Clearing_Enter();
        }

        static bool CheckCellFillARow(GameBusinessContext ctx, int row) {
            var cellRepo = ctx.cellRepo;
            var columns = ctx.currentMapEntity.mapSize.x;
            var map = ctx.currentMapEntity;
            for (int column = 0; column < columns; column++) {
                var pos = GridUtils.GridIndexToPositionInt(column, row, map.mapSize);
                var has = cellRepo.TryGetCellByPos(pos, out var cell);
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
            var cell = cellRepo.DequeueClearingTask();
            GameCellDomain.UnSpawn(ctx, cell);
            if (cellRepo.ClearingTaskCount == 0) {
                game.fsmComponent.Gaming_Enter();
            }
        }

        public static void UnSpawn(GameBusinessContext ctx, CellEntity cell) {
            ctx.cellRepo.Remove(cell);
            cell.TearDown();
        }

    }

}