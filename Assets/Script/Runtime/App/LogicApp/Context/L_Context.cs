using System.Collections;
using System.Collections.Generic;
using Bill.Infra;
using Oshi.Render;
using Oshi.UI;
using UnityEngine;

namespace Oshi.Logic {

    public class L_Context {

        // Domain
        L_AllDomain allDomain;
        public L_AllDomain AllDomain => allDomain;

        // Render
        R_SetterAPI renderSetter;
        public R_SetterAPI RenderSetter => renderSetter;

        // UI
        UI_SetterAPI uiSetter;
        public UI_SetterAPI UISetter => uiSetter;

        // Map
        L_MapEntity mapEntity;
        public L_MapEntity MapEntity => mapEntity;

        // Repo
        L_RoleRepo roleRepo;
        public L_RoleRepo RoleRepo => roleRepo;

        L_PropRepo propRepo;
        public L_PropRepo PropRepo => propRepo;

        L_SolidRepo solidRepo;
        public L_SolidRepo SolidRepo => solidRepo;

        L_AnchorRepo anchorRepo;
        public L_AnchorRepo AnchorRepo => anchorRepo;

        public L_Context() {
            allDomain = new L_AllDomain();
            roleRepo = new L_RoleRepo();
            propRepo = new L_PropRepo();
            solidRepo = new L_SolidRepo();
            anchorRepo = new L_AnchorRepo();
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