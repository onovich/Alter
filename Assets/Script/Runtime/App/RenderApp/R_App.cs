using System.Collections;
using System.Collections.Generic;
using Bill.Infra;
using UnityEngine;

namespace Oshi.Render {

    public class R_App {

        R_SetterAPI setterAPI;
        public R_SetterAPI SetterAPI => setterAPI;

        R_Context context;
        public R_Context Context => context;

        public R_App() {
            setterAPI = new R_SetterAPI();
            context = new R_Context();
        }

        public void Inject(Camera mainCamera,
                           InfraContext infraContext) {
            this.context.Inject(mainCamera, infraContext);
            this.setterAPI.Inject(context);
        }

        public void Init() {
        }

        public void TearDown() {

        }

    }

}
