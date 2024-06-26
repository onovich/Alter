using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Tilemaps;

namespace Alter {

    public class AssetsInfraContext {

        Dictionary<string, GameObject> entityDict;
        public AsyncOperationHandle entityHandle;

        public AssetsInfraContext() {
            entityDict = new Dictionary<string, GameObject>();
        }

        // Entity
        public void Entity_Add(string name, GameObject prefab) {
            entityDict.Add(name, prefab);
        }

        bool Entity_TryGet(string name, out GameObject asset) {
            var has = entityDict.TryGetValue(name, out asset);
            return has;
        }

        public GameObject Entity_GetMap() {
            var has = Entity_TryGet("Entity_Map", out var prefab);
            if (!has) {
                GLog.LogError($"Entity Map not found");
            }
            return prefab;
        }

        public GameObject Entity_GetBlock() {
            var has = Entity_TryGet("Entity_Block", out var prefab);
            if (!has) {
                GLog.LogError($"Entity_Block not found");
            }
            return prefab;
        }

        public GameObject Entity_GetCell() {
            var has = Entity_TryGet("Entity_Cell", out var prefab);
            if (!has) {
                GLog.LogError($"Entity_Cell not found");
            }
            return prefab;
        }

        public void Clear(){
            entityDict.Clear();
        }

    }

}