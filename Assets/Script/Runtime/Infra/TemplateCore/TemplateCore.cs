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
        Dictionary<int, RoleTM> roles;
        Dictionary<int, PropTM> props;
        Dictionary<int, SolidTM> solids;
        Dictionary<int, AnchorTM> anchors;


        public TemplateCore() {
            maps = new Dictionary<int, MapTM>();
            roles = new Dictionary<int, RoleTM>();
            props = new Dictionary<int, PropTM>();
            solids = new Dictionary<int, SolidTM>();
            anchors = new Dictionary<int, AnchorTM>();
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
            var list = await Addressables.LoadAssetsAsync<RoleTM>("SO_Entity", null).Task;
            for (int i = 0; i < list.Count; i++) {
                var entity = list[i];
                roles.Add(entity.typeID, entity);
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

        public RoleTM GetRole(int typeID) {
            if (roles.ContainsKey(typeID)) {
                return roles[typeID];
            } else {
                OshiLog.Error($"Role {typeID} not found");
            }
            return default;
        }

        public PropTM GetProp(int typeID) {
            if (props.ContainsKey(typeID)) {
                return props[typeID];
            } else {
                OshiLog.Error($"Prop {typeID} not found");
            }
            return default;
        }

        public SolidTM GetSolid(int typeID) {
            if (solids.ContainsKey(typeID)) {
                return solids[typeID];
            } else {
                OshiLog.Error($"Solid {typeID} not found");
            }
            return default;
        }

        public AnchorTM GetAnchor(int typeID) {
            if (anchors.ContainsKey(typeID)) {
                return anchors[typeID];
            } else {
                OshiLog.Error($"Anchor {typeID} not found");
            }
            return default;
        }

        public void ClearAll() {
            maps.Clear();
            roles.Clear();
            props.Clear();
            solids.Clear();
            anchors.Clear();
        }

    }

}
