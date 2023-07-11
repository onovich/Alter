using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oshi.Logic {

    public class L_MapDomain {

        L_Context context;

        public void Inject(L_Context context) {
            this.context = context;
        }

        public void CreateMap() {
            var factory = context.Factory;
            factory.CreateMap(1);
            var renderSetter = context.RenderSetter;
            renderSetter.CreateMap(1);
        }

    }

}