using System.Collections;
using System.Collections.Generic;
using Bill.Infra;
using UnityEngine;

namespace Oshi.UI {

    public class UI_Factory {

        UI_Context context;
        InfraContext infraContext;

        public UI_Factory() {
        }

        public void Inject(UI_Context context, InfraContext infraContext) {

            this.context = context;
            this.infraContext = infraContext;

        }

    }

}
