using Bill.Infra;
using Oshi.Logic.Factory;
using Oshi.Render;
using Oshi.UI;

namespace Oshi.Logic {

    public class L_App {

        L_Context context;
        public L_Context Context => context;

        L_Factory factory;
        public L_Factory Factory => factory;

        InfraContext infraContext;
        public InfraContext InfraContext => infraContext;

        public L_App() {
            context = new L_Context();
            factory = new L_Factory();
        }

        public void Inject(InfraContext infraContext, R_SetterAPI renderSetter, UI_SetterAPI uiSetter) {
            this.infraContext = infraContext;
            this.context.Inject(infraContext, renderSetter, uiSetter);
        }

        public void Init() {

        }

        public void TearDown() {

        }

    }

}
