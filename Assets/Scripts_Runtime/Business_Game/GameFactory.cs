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

            // Set Models
            for (int i = 0; i < blockTM.shapeArr.Length; i++) {
                var shapeTM = blockTM.shapeArr[i];
                var shape = new Vector2Int[shapeTM.sizeInt.x * shapeTM.sizeInt.y];
                shapeTM.ForEachCells((index, localPos) => {
                    shape[index] = localPos;
                });
                var shapeModel = new BlockShapeModel {
                    index = i,
                    shape = shape,
                    sizeInt = shapeTM.sizeInt,
                    centerFloat = shapeTM.GetCenterFloat()
                };
                block.shapeComponent.Add(shapeModel);
            }
            return block;
        }

        public static CellEntity Cell_Spawn(IDRecordService idRecordService,
                                               AssetsInfraContext assetsInfraContext,
                                               Vector2Int pos,
                                               Color logicColor) {

            var prefab = assetsInfraContext.Entity_GetCell();
            var cell = GameObject.Instantiate(prefab).GetComponent<CellEntity>();
            cell.Ctor(logicColor);

            // Set ID
            var id = idRecordService.PickCellEntityID();
            cell.entityID = id;

            // Set Pos
            cell.Pos_SetPos(pos);

            return cell;
        }

    }

}