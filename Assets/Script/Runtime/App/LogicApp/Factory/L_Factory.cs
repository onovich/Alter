using System.Collections;
using System.Collections.Generic;
using Bill.Infra;
using UnityEngine;

namespace Oshi.Logic.Factory {

    public class L_Factory {

        L_Context logicContext;
        InfraContext infraContext;

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

        public void CreateEntity(int typeID, Vector2 position) {



            // TM

        }

    }

}