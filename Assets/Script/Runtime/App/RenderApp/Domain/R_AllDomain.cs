using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oshi.Render {

    public class R_AllDomain {

        R_MapDomain mapDomain;
        public R_MapDomain R_MapDomain => mapDomain;

        R_AnchorDomain anchorDomain;
        public R_AnchorDomain R_AnchorDomain => anchorDomain;

        R_PropDomain propDomain;
        public R_PropDomain R_PropDomain => propDomain;

        R_RoleDomain roleDomain;
        public R_RoleDomain R_RoleDomain => roleDomain;

        R_SolidDomain solidDomain;
        public R_SolidDomain R_SolidDomain => solidDomain;

        R_Context context;
        public R_Context Context => context;

        public R_AllDomain() {
            mapDomain = new R_MapDomain();
            anchorDomain = new R_AnchorDomain();
            propDomain = new R_PropDomain();
            roleDomain = new R_RoleDomain();
            solidDomain = new R_SolidDomain();
        }

        public void Inject(R_Context context) {
            this.context = context;
            this.mapDomain.Inject(context);
            this.anchorDomain.Inject(context, this);
            this.propDomain.Inject(context, this);
            this.roleDomain.Inject(context, this);
            this.solidDomain.Inject(context, this);
        }

    }

}