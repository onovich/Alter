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
            var map = ctx.currentMapEntity;
            var mapSize = map.mapSize;
            var mapPos = map.Pos;
            var nextIsInBound = !cell.Move_CheckConstraint(mapSize, mapPos, pos, dir);
            if (nextIsInBound) {
                return;
            }
            cell.Pos_SetPos(cell.PosInt + Vector2Int.down);
        }

    }

}