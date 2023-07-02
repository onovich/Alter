using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oshi.UI {

    public class UI_AllDomain {

        UI_Context context;
        public UI_Context Context => context;

        UI_GameDomain gameDomain;
        public UI_GameDomain GameDomain => gameDomain;

        UI_GameResultDomain gameResultDomain;
        public UI_GameResultDomain GameResultDomain => gameResultDomain;

        UI_LoginDomain loginDomain;
        public UI_LoginDomain LoginDomain => loginDomain;

        UI_SettingDomain settingDomain;
        public UI_SettingDomain SettingDomain => settingDomain;

        public UI_AllDomain() {
            gameDomain = new UI_GameDomain();
            gameResultDomain = new UI_GameResultDomain();
            loginDomain = new UI_LoginDomain();
            settingDomain = new UI_SettingDomain();
        }

        public void Inject(UI_Context context) {
            this.context = context;
            this.gameDomain.Inject(context, this);
            this.gameResultDomain.Inject(context, this);
            this.loginDomain.Inject(context, this);
            this.settingDomain.Inject(context, this);
        }

    }

}