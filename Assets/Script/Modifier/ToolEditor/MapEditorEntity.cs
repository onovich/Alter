using System.Collections;
using System.Collections.Generic;
using Oshi.Generic;
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

            // Role
            model.mapTM.roleArray = GetRoleArray();

            // Prop
            model.mapTM.propArray = GetPropArray();

            // Solid
            model.mapTM.solidArray = GetSolidArray();

            // Anchor
            model.mapTM.anchorArray = GetAnchorArray();

            model.mapTM.id = mapID;
            model.mapTM.size = mapSize;
            model.mapTM.offset = offset;

            EditorUtility.SetDirty(model);
            EditorUtility.SetDirty(this);

            AssetDatabase.SaveAssets();

        }

        RoleSpawnTM[] GetRoleArray() {

            var group = transform.Find("editor_role_group");
            var goes = group.transform.GetComponentsInChildren<RoleEditorElement>();
            if (goes == null || goes.Length == 0) {
                return null;
            }

            var arr = new RoleSpawnTM[goes.Length];
            for (int i = 0; i < goes.Length; i++) {
                var go = goes[i];
                RoleSpawnTM spawner;
                spawner.typeID = go.typeID;
                spawner.position = go.transform.position.ToVector2Int();
                arr[i] = spawner;
            }

            return arr;

        }

        PropSpawnTM[] GetPropArray() {

            var group = transform.Find("editor_prop_group");
            var goes = group.transform.GetComponentsInChildren<PropEditorElement>();
            if (goes == null || goes.Length == 0) {
                return null;
            }

            var arr = new PropSpawnTM[goes.Length];
            for (int i = 0; i < goes.Length; i++) {
                var go = goes[i];
                PropSpawnTM spawner;
                spawner.typeID = go.typeID;
                spawner.position = go.transform.position.ToVector2Int();
                arr[i] = spawner;
            }

            return arr;

        }

        SolidSpawnTM[] GetSolidArray() {

            var group = transform.Find("editor_solid_group");
            var goes = group.transform.GetComponentsInChildren<SolidEditorElement>();
            if (goes == null || goes.Length == 0) {
                return null;
            }

            var arr = new SolidSpawnTM[goes.Length];
            for (int i = 0; i < goes.Length; i++) {
                var go = goes[i];
                SolidSpawnTM spawner;
                spawner.typeID = go.typeID;
                spawner.position = go.transform.position.ToVector2Int();
                arr[i] = spawner;
            }

            return arr;

        }

        AnchorSpawnTM[] GetAnchorArray() {

            var group = transform.Find("editor_anchor_group");
            var goes = group.transform.GetComponentsInChildren<AnchorEditorElement>();
            if (goes == null || goes.Length == 0) {
                return null;
            }

            var arr = new AnchorSpawnTM[goes.Length];
            for (int i = 0; i < goes.Length; i++) {
                var go = goes[i];
                AnchorSpawnTM spawner;
                spawner.typeID = go.typeID;
                spawner.position = go.transform.position.ToVector2Int();
                arr[i] = spawner;
            }

            return arr;

        }

        void OnDrawGizmos() {

            var color = Color.white;
            color.a = 1;

            Gizmos.color = color;
            var size = mapSize.ToVector3();

            Gizmos.DrawWireCube(transform.position, size);

        }

    }

}
