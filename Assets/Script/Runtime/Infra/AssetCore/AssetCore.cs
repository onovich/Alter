using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Oshi.Generic;
using Oshi.Template;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Oshi.Infra {

    public class AssetCore {

        Dictionary<string, GameObject> roleDic;
        public Dictionary<string, GameObject> RoleDic => roleDic;

        Dictionary<string, GameObject> solidDic;
        public Dictionary<string, GameObject> SolidDic => solidDic;

        Dictionary<string, GameObject> anchorDic;
        public Dictionary<string, GameObject> AnchorDic => anchorDic;

        Dictionary<string, GameObject> propDic;
        public Dictionary<string, GameObject> PropDic => propDic;

        Dictionary<string, GameObject> mapDic;
        public Dictionary<string, GameObject> MapDic => mapDic;

        public AssetCore() {
            roleDic = new Dictionary<string, GameObject>();
            solidDic = new Dictionary<string, GameObject>();
            anchorDic = new Dictionary<string, GameObject>();
            propDic = new Dictionary<string, GameObject>();
            mapDic = new Dictionary<string, GameObject>();
        }

        public async Task Init() {

            await LoadRoleAssets();
            await LoadSolidAssets();
            await LoadAnchorAssets();
            await LoadPropAssets();
            await LoadMapAssets();

        }

        public async Task LoadRoleAssets() {
            AssetLabelReference labelReference = new AssetLabelReference();
            labelReference.labelString = "SO_Role";
            var list = await Addressables.LoadAssetsAsync<GameObject>(labelReference, null).Task;
            for (int i = 0; i < list.Count; i++) {
                var go = list[i];
                roleDic.Add(go.name, go);
            }
        }

        public async Task LoadSolidAssets() {
            AssetLabelReference labelReference = new AssetLabelReference();
            labelReference.labelString = "SO_Solid";
            var list = await Addressables.LoadAssetsAsync<GameObject>(labelReference, null).Task;
            for (int i = 0; i < list.Count; i++) {
                var go = list[i];
                roleDic.Add(go.name, go);
            }
        }

        public async Task LoadAnchorAssets() {
            AssetLabelReference labelReference = new AssetLabelReference();
            labelReference.labelString = "SO_Anchor";
            var list = await Addressables.LoadAssetsAsync<GameObject>(labelReference, null).Task;
            for (int i = 0; i < list.Count; i++) {
                var go = list[i];
                roleDic.Add(go.name, go);
            }
        }

        public async Task LoadPropAssets() {
            AssetLabelReference labelReference = new AssetLabelReference();
            labelReference.labelString = "SO_Prop";
            var list = await Addressables.LoadAssetsAsync<GameObject>(labelReference, null).Task;
            for (int i = 0; i < list.Count; i++) {
                var go = list[i];
                roleDic.Add(go.name, go);
            }
        }

        public async Task LoadMapAssets() {
            AssetLabelReference labelReference = new AssetLabelReference();
            labelReference.labelString = "SO_Map";
            var list = await Addressables.LoadAssetsAsync<GameObject>(labelReference, null).Task;
            for (int i = 0; i < list.Count; i++) {
                var go = list[i];
                roleDic.Add(go.name, go);
            }
        }

        public GameObject GetEntity(string name) {
            if (roleDic.ContainsKey(name)) {
                return roleDic[name];
            } else {
                OshiLog.Error($"Entity {name} not found");
            }
            return null;
        }

        public GameObject GetMap(int mapID) {
            var name = $"go_map_{mapID}";
            if (mapDic.ContainsKey(name)) {
                return mapDic[name];
            }
            return null;
        }

        public GameObject GetRole(int roleTypeID) {
            var name = $"go_role_{roleTypeID}";
            if (roleDic.ContainsKey(name)) {
                return roleDic[name];
            }
            return null;
        }

        public GameObject GetProp(int propTypeID) {
            var name = $"go_prop_{propTypeID}";
            if (propDic.ContainsKey(name)) {
                return propDic[name];
            }
            return null;
        }

        public GameObject GetAnchor(int anchorTypeID) {
            var name = $"go_anchor_{anchorTypeID}";
            if (anchorDic.ContainsKey(name)) {
                return anchorDic[name];
            }
            return null;
        }

        public GameObject GetSolid(int solidTypeID) {
            var name = $"go_solid_{solidTypeID}";
            if (anchorDic.ContainsKey(name)) {
                return anchorDic[name];
            }
            return null;
        }

        public void ClearAll() {
            roleDic.Clear();
            mapDic.Clear();
            propDic.Clear();
            solidDic.Clear();
            anchorDic.Clear();
        }

    }

}
