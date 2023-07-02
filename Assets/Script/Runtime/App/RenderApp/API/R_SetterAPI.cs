using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oshi.Render {

    public class R_SetterAPI {

        R_Context context;

        public R_SetterAPI() {
        }

        public void Inject(R_Context context) {
            this.context = context;
        }

    }

}