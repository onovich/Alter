using System.Collections;
using System.Collections.Generic;
using Bill.Infra;
using UnityEngine;

namespace Oshi.Render {

    public class R_Factory {

        R_Context context;
        InfraContext infraContext;

        public R_Factory() {
        }

        public void Inject(R_Context context, InfraContext infraContext) {
            this.context = context;
            this.infraContext = infraContext;
        }

        public void CreateMap(int mapID) {

            // TM
            var templateCore = infraContext.TemplateCore;
            var mapTM = templateCore.GetMap(mapID);

            // GO
            var assetCore = infraContext.AssetCore;
            var prefab = assetCore.GetMap(mapID);
            var root = context.WorldRoot;
            var go = GameObject.Instantiate(prefab, root);

            // Entity
            var entity = go.GetComponent<R_MapEntity>();
            entity.Ctor();

            // Init
            var size = mapTM.size;
            var offset = mapTM.offset;
            entity.Init(mapID, size, offset);

        }

        public void CreateRole(int typeID, Vector2Int pos, Transform parent) {

            // TM
            var templateCore = infraContext.TemplateCore;
            var roleTM = templateCore.GetRole(typeID);

            // GO
            var assetCore = infraContext.AssetCore;
            var prefab = assetCore.GetRole(typeID);
            var go = GameObject.Instantiate(prefab, parent);

            // Entity
            var entity = go.GetComponent<R_RoleEntity>();
            entity.Ctor();

            // Init
            var id = 0;
            entity.Init(id, typeID, pos);

        }

        public void CreateSolid(int typeID, Vector2Int pos, Transform parent) {

            // TM
            var templateCore = infraContext.TemplateCore;
            var solidTM = templateCore.GetSolid(typeID);

            // GO
            var assetCore = infraContext.AssetCore;
            var prefab = assetCore.GetSolid(typeID);
            var go = GameObject.Instantiate(prefab, parent);

            // Entity
            var entity = go.GetComponent<R_SolidEntity>();
            entity.Ctor();

            // Init
            var id = 0;
            entity.Init(id, typeID, pos);

        }

        public void CreateAnchor(int typeID, Vector2Int pos, Transform parent) {

            // TM
            var templateCore = infraContext.TemplateCore;
            var solidTM = templateCore.GetAnchor(typeID);

            // GO
            var assetCore = infraContext.AssetCore;
            var prefab = assetCore.GetAnchor(typeID);
            var go = GameObject.Instantiate(prefab, parent);

            // Entity
            var entity = go.GetComponent<R_AnchorEntity>();
            entity.Ctor();

            // Init
            var id = 0;
            entity.Init(id, typeID, pos);

        }

        public void CreateProp(int typeID, Vector2Int pos, Transform parent) {

            // TM
            var templateCore = infraContext.TemplateCore;
            var solidTM = templateCore.GetProp(typeID);

            // GO
            var assetCore = infraContext.AssetCore;
            var prefab = assetCore.GetProp(typeID);
            var go = GameObject.Instantiate(prefab, parent);

            // Entity
            var entity = go.GetComponent<R_PropEntity>();
            entity.Ctor();

            // Init
            var id = 0;
            entity.Init(id, typeID, pos);

        }

    }

}