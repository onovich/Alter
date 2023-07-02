using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Oshi.Infra;
using UnityEngine;

namespace Bill.Infra {

    public class InfraContext {

        public AssetCore AssetCore;
        public TemplateCore TemplateCore;
        public EventCore EventCore;

        public void Inject(AssetCore assetCore, TemplateCore templateCore, EventCore eventCore) {
            this.AssetCore = assetCore;
            this.TemplateCore = templateCore;
            this.EventCore = eventCore;
        }

        public async Task Init() {

            await AssetCore.Init();
            await TemplateCore.Init();

        }

        public void TearDown() {

        }

    }

}