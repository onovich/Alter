using UnityEngine;

namespace Alter {

    public static class GameBlockDomain {

        public static BlockEntity Spawn(GameBusinessContext ctx, int typeID, int index, Vector2Int pos) {
            var block = GameFactory.Block_Spawn(ctx.templateInfraContext,
                                              ctx.assetsInfraContext,
                                              typeID,
                                              index,
                                              pos);

            ctx.blockRepo.Add(block);
            return block;
        }

        public static void UnSpawn(GameBusinessContext ctx, BlockEntity block) {
            ctx.blockRepo.Remove(block);
            block.TearDown();
        }

        public static void ApplyFalling(GameBusinessContext ctx, BlockEntity block) {
            block.Pos_SetPos(block.Pos + Vector2.down);
        }

    }

}