using System.Collections;
using System.Collections.Generic;
using Bill.Infra;
using UnityEngine;

namespace Oshi.Render {

    public class R_Context {

        // Camera
        Camera mainCamera;
        public Camera MainCamera => mainCamera;

        // Repo

        // Domain
        R_AllDomain allDomain;
        public R_AllDomain AllDomain => allDomain;

        public R_Context() {
            allDomain = new R_AllDomain();
        }

        public void Inject(Camera mainCamera,
                           InfraContext infraContext) {
            this.mainCamera = mainCamera;
            this.allDomain.Inject(this);
        }

    }

}