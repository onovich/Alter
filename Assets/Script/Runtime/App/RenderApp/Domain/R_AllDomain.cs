using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oshi.Render {

    public class R_AllDomain {

        R_EntityDomain entityDomain;
        public R_EntityDomain EntityDomain => entityDomain;

        R_Context context;
        public R_Context Context => context;

        public R_AllDomain() {
            entityDomain = new R_EntityDomain();
        }

        public void Inject(R_Context context) {
            this.context = context;
            this.entityDomain.Inject(context, this);
        }

    }

}