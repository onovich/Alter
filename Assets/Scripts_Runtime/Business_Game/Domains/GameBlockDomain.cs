using UnityEngine;

namespace Alter {

    public static class GameBlockDomain {

        public static BlockEntity Spawn(GameBusinessContext ctx, int typeID, int index, Vector2Int pos, Vector2Int size) {
            var block = GameFactory.Block_Spawn(ctx.templateInfraContext,
                                              ctx.assetsInfraContext,
                                              typeID,
                                              index,
                                              pos,
                                              size);

            ctx.blockRepo.Add(block);
            return block;
        }

        public static void UnSpawn(GameBusinessContext ctx, BlockEntity block) {
            ctx.blockRepo.Remove(block);
            block.TearDown();
        }

        static void ResetBlock(GameBusinessContext ctx, BlockEntity block) {
            var originalPos = block.originalPos;
            var oldPos = block.PosInt;
            block.Pos_SetPos(originalPos);
            ctx.blockRepo.UpdatePos(oldPos, block);
        }

    }

}