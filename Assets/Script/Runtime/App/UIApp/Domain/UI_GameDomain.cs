using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oshi.UI {

    public class UI_GameDomain {

        UI_Context context;
        UI_AllDomain allDomain;
        public void Inject(UI_Context context, UI_AllDomain allDomain) {
            this.context = context;
            this.allDomain = allDomain;
        }

    }

}
