using Bill.Infra;
using UnityEngine;

namespace Oshi.UI {

    public class UI_App {

        UI_SetterAPI setterAPI;
        public UI_SetterAPI SetterAPI => setterAPI;

        UI_Context context;
        public UI_Context Context => context;

        UI_Factory factory;
        public UI_Factory Factory => factory;

        public UI_App() {
            setterAPI = new UI_SetterAPI();
            context = new UI_Context();
            factory = new UI_Factory();
        }

        public void Inject(Canvas canvas, Camera uiCamera, InfraContext infraContext) {

            this.context.Inject(canvas, uiCamera, infraContext);
            this.setterAPI.Inject(context);
            this.factory.Inject(context, infraContext);

        }

        public void Init() {

        }

        public void TearDown() {

        }

    }

}