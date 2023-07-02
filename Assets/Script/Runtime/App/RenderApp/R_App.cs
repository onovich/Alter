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

        R_Factory factory;
        public R_Factory Factory => factory;

        public R_App() {
            setterAPI = new R_SetterAPI();
            context = new R_Context();
            factory = new R_Factory();
        }

        public void Inject(Camera mainCamera,
                           InfraContext infraContext) {
            this.context.Inject(mainCamera, infraContext);
            this.setterAPI.Inject(context);
            this.factory.Inject(context, infraContext);
        }

        public void Init() {
        }

        public void TearDown() {

        }

    }

}
