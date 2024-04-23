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

        public static BlockEntity Block_Spawn(IDRecordService idRecordService,
                                              TemplateInfraContext templateInfraContext,
                                              AssetsInfraContext assetsInfraContext,
                                              Vector2Int pos) {

            var prefab = assetsInfraContext.Entity_GetBlock();
            var block = GameObject.Instantiate(prefab).GetComponent<BlockEntity>();
            block.Ctor();

            // Set ID
            var id = idRecordService.PickBlockEntityID();
            block.entityID = id;

            // Set Pos
            block.Pos_SetPos(pos);

            // Set Bounds
            var has = templateInfraContext.Block_TryGet(id, out var blockTM);
            if (!has) {
                GLog.LogError($"Block {id} not found");
            }

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
                    centerInt = shapeTM.centerInt,
                    centerFloat = shapeTM.GetCenterFloat()
                };
                block.shapeComponent.Add(shapeModel);
            }
            return block;
        }

        public static CellEntity Cell_Spawn(IDRecordService idRecordService,
                                               AssetsInfraContext assetsInfraContext,
                                               Vector2Int pos) {

            var prefab = assetsInfraContext.Entity_GetCell();
            var cell = GameObject.Instantiate(prefab).GetComponent<CellEntity>();
            cell.Ctor();

            // Set ID
            var id = idRecordService.PickCellEntityID();
            cell.entityID = id;

            // Set Pos
            cell.Pos_SetPos(pos);

            return cell;
        }

    }

}