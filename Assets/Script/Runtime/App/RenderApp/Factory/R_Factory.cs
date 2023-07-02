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

    }

}