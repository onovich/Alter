using System.Collections;
using System.Collections.Generic;
using Bill.Infra;
using Oshi.Logic;
using Oshi.Render;
using Oshi.UI;
using UnityEngine;

namespace Oshi {

    public class GameController {

        InfraContext infraContext;
        UI_App uiApp;
        L_App logicApp;
        R_App renderApp;

        public GameController() {
            // 1. 初始化地图
            // 2. 初始化角色
            // 3. 初始化箱子
            // 4. 初始化目标点
            // 5. 初始化地图
        }

        public void Init() {

        }

        public void Enter() {
            var mapDomain = logicApp.Context.AllDomain.MapDomain;
            mapDomain.CreateMap();
        }

        public void Exit() {

        }

        public void Inject(InfraContext infraContext, UI_App uiApp, L_App logicApp, R_App renderApp) {

            this.infraContext = infraContext;
            this.uiApp = uiApp;
            this.logicApp = logicApp;
            this.renderApp = renderApp;

        }

        public void Tick(float dt) {
            // 1.记录输入
            // 2. 判定可推
            // 3. 判定可行走
            // 4. 执行箱子位移
            // 5. 执行角色位移
            // 6. 判定成功
            // 7. 判定过关
        }

        public void TearDown() {

        }

    }

}

