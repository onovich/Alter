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

        public static void UnSpawn(GameBusinessContext ctx, CellEntity cell) {
            ctx.cellRepo.Remove(cell);
            cell.TearDown();
        }

    }

}