using UnityEngine;

namespace Alter {

    public static class GameCellDomain {

        public static CellSubEntity Spawn(GameBusinessContext ctx, Vector2Int pos) {
            var cell = GameFactory.Cell_Spawn(ctx.idRecordService,
                                              ctx.assetsInfraContext,
                                              pos);

            ctx.cellRepo.Add(cell);
            return cell;
        }

        public static void UnSpawn(GameBusinessContext ctx, CellSubEntity cell) {
            ctx.cellRepo.Remove(cell);
            cell.TearDown();
        }

        public static void ApplyFalling(GameBusinessContext ctx, CellSubEntity cell) {
            cell.Pos_SetPos(cell.PosInt + Vector2Int.down);
        }

    }

}