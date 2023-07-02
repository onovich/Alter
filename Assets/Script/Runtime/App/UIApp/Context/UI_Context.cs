using System.Collections;
using System.Collections.Generic;
using Bill.Infra;
using UnityEngine;

namespace Oshi.UI {

    public class UI_Context {

        // Camera
        Camera uiCamera;
        public Camera UICamera => uiCamera;

        // Repo

        // Domain
        UI_AllDomain allDomain;
        public UI_AllDomain AllDomain => allDomain;

        public UI_Context() {
            allDomain = new UI_AllDomain();
        }

        public void Inject(Canvas canvas,
                           Camera uiCamera,
                           InfraContext infraContext) {
            this.uiCamera = uiCamera;
            this.allDomain.Inject(this);
        }

    }

}