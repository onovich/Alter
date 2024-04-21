using UnityEngine;

namespace Alter {

    public static class GameCellDomain {

        public static CellEntity Spawn(GameBusinessContext ctx, Vector2Int pos) {
            var cell = GameFactory.Cell_Spawn(ctx.idRecordService,
                                              ctx.assetsInfraContext,
                                              pos);

            ctx.cellRepo.Add(cell);
            return cell;
        }

        public static void UnSpawn(GameBusinessContext ctx, CellEntity cell) {
            ctx.cellRepo.Remove(cell);
            cell.TearDown();
        }

        public static void ApplyFalling(GameBusinessContext ctx, CellEntity cell) {
            if (!ctx.gameEntity.IsFallingFrame) {
                return;
            }
            var dir = Vector2Int.down;
            var pos = cell.PosInt;
            cell.Pos_SetPos(pos + dir);
        }

        public static void ApplyMove(GameBusinessContext ctx, CellEntity cell, Vector2Int dir) {
            var pos = cell.PosInt;
            var allow = cell.Move_CheckInConstraint(ctx.currentMapEntity.mapSize,
                                                   ctx.currentMapEntity.Pos,
                                                   pos,
                                                   dir);
            if (!allow) {
                return;
            }
            cell.Pos_SetPos(pos + dir);
        }

        public static void ApplyAllLand(GameBusinessContext ctx) {
            var cellLen = ctx.cellRepo.TakeAll(out var cellArr);
            for (int i = 0; i < cellLen; i++) {
                var cell = cellArr[i];
                cell.fsmComponent.Landing_Enter();
            }
        }

        public static void ApplyCheckAllCellLanding(GameBusinessContext ctx) {
            if (!ctx.gameEntity.IsFallingFrame) {
                return;
            }
            var cellLen = ctx.cellRepo.TakeAll(out var cellArr);
            bool notInLand = true;
            for (int i = 0; i < cellLen; i++) {
                var cell = cellArr[i];
                if (cell.fsmComponent.Status == CellFSMStatus.Landing) {
                    continue;
                }
                notInLand &= GameCellDomain.CheckInConstraint(ctx, cell);
                notInLand &= GameCellDomain.CheckNextIsNotLandingCell(ctx, cell);
                if (!notInLand) {
                    break;
                }
            }

            if (notInLand) {
                return;
            }
            for (int i = 0; i < cellLen; i++) {
                var cell = cellArr[i];
                cell.fsmComponent.Landing_Enter();
            }
        }

        static bool CheckInConstraint(GameBusinessContext ctx, CellEntity cell) {
            var dir = Vector2Int.down;
            var pos = cell.PosInt;
            var map = ctx.currentMapEntity;
            var mapSize = map.mapSize;
            var mapPos = map.Pos;
            return cell.Move_CheckInAir(mapSize, mapPos, pos, dir);
        }

        static bool CheckNextIsNotLandingCell(GameBusinessContext ctx, CellEntity cell) {
            var dir = Vector2Int.down;
            var pos = cell.PosInt;
            var nextPos = pos + dir;
            var has = ctx.cellRepo.TryGetBlockByPos(nextPos, out var _);
            if (!has) {
                return true;
            }
            if (cell.fsmComponent.Status == CellFSMStatus.Landing) {
                return false;
            }
            return true;
        }

    }

}