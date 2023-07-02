using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Oshi.Generic;
using Oshi.Template;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Oshi.Infra {

    public class AssetCore {

        Dictionary<string, GameObject> entityDic;
        public Dictionary<string, GameObject> EntityDic => entityDic;
        Dictionary<string, GameObject> mapDic;
        public Dictionary<string, GameObject> MapDic => mapDic;

        public AssetCore() {
            entityDic = new Dictionary<string, GameObject>();
            mapDic = new Dictionary<string, GameObject>();
        }

        public async Task Init() {

            await LoadEntityAssets();
            await LoadMapAssets();

        }

        public async Task LoadEntityAssets() {
            AssetLabelReference labelReference = new AssetLabelReference();
            labelReference.labelString = "SO_Entity";
            var list = await Addressables.LoadAssetsAsync<GameObject>(labelReference, null).Task;
            for (int i = 0; i < list.Count; i++) {
                var go = list[i];
                entityDic.Add(go.name, go);
            }
        }

        public async Task LoadMapAssets() {
            AssetLabelReference labelReference = new AssetLabelReference();
            labelReference.labelString = "SO_Map";
            var list = await Addressables.LoadAssetsAsync<GameObject>(labelReference, null).Task;
            for (int i = 0; i < list.Count; i++) {
                var go = list[i];
                entityDic.Add(go.name, go);
            }
        }

        public GameObject GetEntity(string name) {
            if (entityDic.ContainsKey(name)) {
                return entityDic[name];
            } else {
                OshiLog.Error($"Entity {name} not found");
            }
            return null;
        }

        public GameObject GetMap(string name) {
            if (mapDic.ContainsKey(name)) {
                return mapDic[name];
            }
            return null;
        }

        public void ClearAll() {
            entityDic.Clear();
            mapDic.Clear();
        }

    }

}
