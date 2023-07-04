using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oshi.Logic {

    public class L_AllDomain {

        L_MapDomain mapDomain;
        public L_MapDomain MapDomain => mapDomain;

        public L_AllDomain() {
            mapDomain = new L_MapDomain();
        }

        public void Inject(L_Context context) {
            mapDomain.Inject(context, this);
        }

    }

}