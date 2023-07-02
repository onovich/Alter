using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Oshi.Generic;
using Oshi.Template;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Oshi.Infra {
    public class TemplateCore {

        Dictionary<int, MapTM> maps;
        Dictionary<int, EntityTM> entities;

        public TemplateCore() {
            maps = new Dictionary<int, MapTM>();
            entities = new Dictionary<int, EntityTM>();
        }

        public async Task Init() {

            await LoadMaps();
            await LoadEntities();

        }

        public async Task LoadMaps() {
            var list = await Addressables.LoadAssetsAsync<MapTM>("SO_Map", null).Task;
            for (int i = 0; i < list.Count; i++) {
                var map = list[i];
                maps.Add(map.id, map);
            }
        }

        public async Task LoadEntities() {
            var list = await Addressables.LoadAssetsAsync<EntityTM>("SO_Entity", null).Task;
            for (int i = 0; i < list.Count; i++) {
                var entity = list[i];
                entities.Add(entity.typeID, entity);
            }
        }

        public MapTM GetMap(int id) {
            if (maps.ContainsKey(id)) {
                return maps[id];
            } else {
                OshiLog.Error($"Map {id} not found");
            }
            return default;
        }

        public EntityTM GetEntity(int typeID) {
            if (entities.ContainsKey(typeID)) {
                return entities[typeID];
            } else {
                OshiLog.Error($"Entity {typeID} not found");
            }
            return default;
        }

        public void ClearAll() {
            maps.Clear();
            entities.Clear();
        }

    }

}
