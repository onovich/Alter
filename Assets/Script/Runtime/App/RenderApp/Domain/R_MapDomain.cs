using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oshi.Render {

    public class R_MapDomain {

        R_Context context;

        public R_MapDomain() {
        }

        public void Inject(R_Context context) {
            this.context = context;
        }

        public void CreateMap(int mapID) {
            var factory = context.Factory;
            factory.CreateMap(mapID);
        }

    }

}
