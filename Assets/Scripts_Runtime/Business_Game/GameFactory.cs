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

        public static CellSubEntity Cell_Spawn(IDRecordService idRecordService,
                                               AssetsInfraContext assetsInfraContext,
                                               Vector2Int pos) {

            var prefab = assetsInfraContext.Entity_GetCell();
            var cell = GameObject.Instantiate(prefab).GetComponent<CellSubEntity>();
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