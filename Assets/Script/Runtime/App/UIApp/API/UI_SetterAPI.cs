using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oshi.UI {

    public class UI_SetterAPI {

        UI_Context context;

        public UI_SetterAPI() {
        }

        public void Inject(UI_Context context) {
            this.context = context;
        }

    }

}