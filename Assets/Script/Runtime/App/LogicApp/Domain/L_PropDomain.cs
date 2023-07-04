using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oshi.Logic {

    public class L_PropDomain {

        L_Context context;
        L_AllDomain allDomain;

        public void Inject(L_Context context, L_AllDomain allDomain) {
            this.context = context;
            this.allDomain = allDomain;
        }

    }

}