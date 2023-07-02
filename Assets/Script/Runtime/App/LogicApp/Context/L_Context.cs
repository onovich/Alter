using System.Collections;
using System.Collections.Generic;
using Bill.Infra;
using Oshi.Render;
using Oshi.UI;
using UnityEngine;

namespace Oshi.Logic {

    public class L_Context {

        // Repo

        // Domain
        L_AllDomain allDomain;
        public L_AllDomain AllDomain => allDomain;

        // Render
        R_SetterAPI renderSetter;
        public R_SetterAPI RenderSetter => renderSetter;

        // UI
        UI_SetterAPI uiSetter;
        public UI_SetterAPI UISetter => uiSetter;

        public L_Context() {
            allDomain = new L_AllDomain();
        }

        public void Inject(InfraContext infraContext,
                           R_SetterAPI renderSetter,
                           UI_SetterAPI uiSetter) {
            this.allDomain.Inject(this);
            this.renderSetter = renderSetter;
            this.uiSetter = uiSetter;
        }

    }

}