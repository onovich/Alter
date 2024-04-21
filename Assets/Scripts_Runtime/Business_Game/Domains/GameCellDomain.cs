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
            var dir = Vector2Int.down;
            var pos = cell.PosInt;
            cell.Pos_SetPos(pos + dir);
        }

        public static void ApplyAllLand(GameBusinessContext ctx) {
            var cellLen = ctx.cellRepo.TakeAll(out var cellArr);
            for (int i = 0; i < cellLen; i++) {
                var cell = cellArr[i];
                cell.fsmComponent.Landing_Enter();
            }
        }

        public static bool CheckConstraint(GameBusinessContext ctx, CellEntity cell) {
            var dir = Vector2Int.down;
            var pos = cell.PosInt;
            var map = ctx.currentMapEntity;
            var mapSize = map.mapSize;
            var mapPos = map.Pos;
            return cell.Move_CheckConstraint(mapSize, mapPos, pos, dir);
        }

        public static bool CheckNextIsLandingCell(GameBusinessContext ctx, CellEntity cell) {
            var dir = Vector2Int.down;
            var pos = cell.PosInt;
            var nextPos = pos + dir;
            var has = ctx.cellRepo.TryGetBlockByPos(nextPos, out var _);
            if (!has) {
                return false;
            }
            if (cell.fsmComponent.Status == CellFSMStatus.Landing) {
                return true;
            }
            return false;
        }

    }

}