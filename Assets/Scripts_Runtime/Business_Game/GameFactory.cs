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
            map.previewPoint = mapTM.previewPoint;

            return map;
        }

        public static BlockEntity Block_Spawn(IDRecordService idRecordService,
                                              int typeID,
                                              TemplateInfraContext templateInfraContext,
                                              AssetsInfraContext assetsInfraContext,
                                              Vector2Int pos) {

            var prefab = assetsInfraContext.Entity_GetBlock();
            var block = GameObject.Instantiate(prefab).GetComponent<BlockEntity>();
            block.Ctor();

            var has = templateInfraContext.Block_TryGet(typeID, out var blockTM);
            if (!has) {
                GLog.LogError($"Block {typeID} not found");
            }

            // Set ID
            var id = idRecordService.PickBlockEntityID();
            block.entityID = id;
            block.typeID = typeID;
            block.typeName = blockTM.typeName;

            // Set Pos
            block.Pos_SetPos(pos);

            // Set Model
            var shapeTM = blockTM.shape;
            var shapeModel = new BlockShapeModel {
                shape = shapeTM.cells,
                sizeInt = shapeTM.sizeInt,
                centerFloat = shapeTM.centerFloat
            };
            block.shapeComponent.shape = shapeModel;
            return block;
        }

        public static CellEntity Cell_Spawn(IDRecordService idRecordService,
                                               AssetsInfraContext assetsInfraContext,
                                               Vector2Int pos,
                                               int index) {

            var prefab = assetsInfraContext.Entity_GetCell();
            var cell = GameObject.Instantiate(prefab).GetComponent<CellEntity>();
            cell.Ctor();

            // Set ID
            cell.index = index;

            // Set Pos
            cell.Pos_SetPos(pos);

            return cell;
        }

    }

}