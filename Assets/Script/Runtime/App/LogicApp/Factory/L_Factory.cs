using System.Collections;
using System.Collections.Generic;
using Bill.Infra;
using Oshi.Template;
using UnityEngine;

namespace Oshi.Logic.Factory {

    public class L_Factory {

        L_Context logicContext;
        InfraContext infraContext;

        public L_Factory() {
        }

        public void Inject(L_Context logicContext, InfraContext infraContext) {
            this.logicContext = logicContext;
            this.infraContext = infraContext;
        }

        public void CreateMap(int mapID) {

            // TM
            var templateCore = infraContext.TemplateCore;
            var mapTM = templateCore.GetMap(mapID);

            // Entity
            var entity = new L_MapEntity();
            entity.Ctor();

            entity.FromTM(mapTM);

        }

        public L_RoleEntity CreateRole(RoleSpawnTM spawnTM) {

            // Args
            var typeID = spawnTM.typeID;
            var position = spawnTM.position;

            // TM
            var templateCore = infraContext.TemplateCore;
            var roleTM = templateCore.GetRole(typeID);

            // Entity
            var entity = new L_RoleEntity();
            entity.Ctor();

            // From TM
            entity.FromTM(roleTM);

            // From Spawn TM
            entity.SetPos(position);

            return entity;

        }

        public L_PropEntity CreateProp(PropSpawnTM spawnTM) {

            // Args
            var typeID = spawnTM.typeID;
            var position = spawnTM.position;

            // TM
            var templateCore = infraContext.TemplateCore;
            var propTM = templateCore.GetProp(typeID);

            // Entity
            var entity = new L_PropEntity();
            entity.Ctor();

            // From TM
            entity.FromTM(propTM);

            // From Spawn TM
            entity.SetPos(position);

            return entity;

        }

    }

}