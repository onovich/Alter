using System.Collections;
using System.Collections.Generic;
using Bill.Infra;
using Oshi.UI;
using UnityEngine;

namespace Oshi {

    public class LoginController {

        InfraContext infraContext;
        UI_App uiApp;

        public void Init() {

        }

        public void Enter() {

        }

        public void Exit() {

        }

        public void Inject(InfraContext infraContext, UI_App uiApp) {

            this.infraContext = infraContext;
            this.uiApp = uiApp;

        }

        public void TearDown() {

        }

    }

}
