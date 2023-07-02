using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oshi.Logic {

    public class L_AllDomain {

        L_EntityDomain entityDomain;
        public L_EntityDomain EntityDomain => entityDomain;

        public L_AllDomain() {
            entityDomain = new L_EntityDomain();
        }

        public void Inject(L_Context context) {
            entityDomain.Inject(context, this);
        }

    }

}