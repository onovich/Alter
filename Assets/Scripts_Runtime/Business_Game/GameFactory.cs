using UnityEngine;

namespace Alter {

    public static class GameFactory {

        public static MapEntity Map_Spawn(TemplateInfraContext templateInfraContext,
                                 AssetsInfraContext assetsInfraContext,
                                 int typeID) {

            var has = templateInfraContext.Map_TryGet(typeID, out var mapTM);
            if (!has) {
                GLog.LogError($"Map {typeID} not found");
            }

            var prefab = assetsInfraContext.Entity_GetMap();
            var map = GameObject.Instantiate(prefab).GetComponent<MapEntity>();
            map.Ctor();
            map.typeID = typeID;
            map.mapSize = mapTM.mapSize;

            // Set Mesh
            map.Mesh_SetSize(map.mapSize);

            // Set Point
            map.spawnPoint = mapTM.spawnPoint;

            return map;
        }


        public static BlockEntity Block_Spawn(TemplateInfraContext templateInfraContext,
                                 AssetsInfraContext assetsInfraContext,
                                 int typeID,
                                 int index,
                                 Vector2Int pos) {

            var has = templateInfraContext.Block_TryGet(typeID, out var blockTM);
            if (!has) {
                GLog.LogError($"Block {typeID} not found");
            }

            var prefab = assetsInfraContext.Entity_GetBlock();
            var block = GameObject.Instantiate(prefab).GetComponent<BlockEntity>();
            block.Ctor();

            // Base Info
            block.typeID = typeID;
            block.entityIndex = index;
            block.typeName = blockTM.typeName;

            // Rename
            block.gameObject.name = $"Block - {block.typeName} - {block.entityIndex}";

            // Set Pos
            block.Pos_SetPos(pos);
            block.originalPos = pos;

            // Set Cell
            blockTM.ForEachCellsLocalPos(localPos => {
                var cellPos = pos + localPos;
                var cell = SpawnCellSubEntity(assetsInfraContext, cellPos, block);
            });

            return block;
        }

        static CellSubEntity SpawnCellSubEntity(AssetsInfraContext assetsInfraContext,
                                                Vector2Int pos,
                                                BlockEntity block) {

            var prefab = assetsInfraContext.Entity_GetCell();
            var cell = GameObject.Instantiate(prefab).GetComponent<CellSubEntity>();
            cell.Ctor();

            // Set Pos
            cell.Pos_SetPos(pos);

            // Set Parent
            block.AddCell(cell);

            return cell;
        }

    }

}