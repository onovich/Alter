using System.Collections;
using System.Collections.Generic;
using Oshi.Template;
using UnityEditor;
using UnityEngine;

namespace Oshi.Modifier {

    public class MapEditorEntity : MonoBehaviour {

        public int mapID;
        public MapSO model;
        public Vector2Int mapSize;
        public Vector2 offset;

        [ContextMenu("SaveData")]
        void SaveData() {

            if (model == null) {
                model = ScriptableObject.CreateInstance<MapSO>();
                model.name = $"so_map_{mapID}";
                AssetDatabase.CreateAsset(model, $"Assets/Template/Map/so_battle_{mapID}.asset");
                return;
            }

            // Entity
            model.mapTM.entities = GetEntityArray();

            model.mapTM.id = mapID;
            model.mapTM.size = mapSize;
            model.mapTM.offset = offset;

            EditorUtility.SetDirty(model);
            EditorUtility.SetDirty(this);

            AssetDatabase.SaveAssets();

        }

        EntitySpawnTM[] GetEntityArray() {

            var group = transform.Find("editor_entity_group");
            var goes = group.transform.GetComponentsInChildren<EntityEditorElement>();
            if (goes == null || goes.Length == 0) {
                return null;
            }

            var arr = new EntitySpawnTM[goes.Length];
            for (int i = 0; i < goes.Length; i++) {
                var go = goes[i];
                EntitySpawnTM spawner;
                spawner.typeID = go.typeID;
                spawner.position = go.transform.position;
                arr[i] = spawner;
            }

            return arr;

        }

    }

}
