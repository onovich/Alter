using UnityEngine;

namespace Alter {

    public static class GameBlockDomain {

        public static void SpawnCellArrFromBlock(GameBusinessContext ctx, int typeID, Vector2Int pos) {
            var has = ctx.templateInfraContext.Block_TryGet(typeID, out var blockTM);
            if (!has) {
                GLog.LogError($"Block {typeID} not found");
                return;
            }
            blockTM.ForEachCellsLocalPos((localPos) => {
                var cellPos = pos + localPos;
                var cell = GameCellDomain.Spawn(ctx, cellPos);
            });
        }

    }

}