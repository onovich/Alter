using UnityEngine;
using Oshi.Infra;
using Oshi.Generic;
using Oshi.Logic;
using Oshi.Render;
using Oshi.UI;
using Bill.Infra;
using System;

namespace Oshi {

    public class Main : MonoBehaviour {

        [SerializeField] OshiLog.LogLevel logLevel;

        bool isInit;
        bool isTearDown;

        // Infra 
        InfraContext infraContext;
        AssetCore assetCore;
        TemplateCore templateCore;
        EventCore eventCore;

        // Controller
        LoginController loginController;
        GameController gameController;

        // Application
        UI_App uiApp;
        L_App logicApp;
        R_App renderApp;

        void Awake() {

            // Loggier
            OshiLog.AllowCache = false;
            OshiLog.Level = logLevel;
            OshiLog.OnLog += Debug.Log;
            OshiLog.OnWarning += Debug.LogWarning;
            OshiLog.OnError += Debug.LogError;
            OshiLog.OnAssert += (condition, msg) => Debug.Assert(condition, msg);
            OshiLog.OnAssertWithoutMessage += (condition) => Debug.Assert(condition);

            // Init
            assetCore = new AssetCore();
            templateCore = new TemplateCore();
            eventCore = new EventCore();

            // Canvas
            var canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();

            // Inra
            infraContext = new InfraContext();

            // Application
            uiApp = new UI_App();
            logicApp = new L_App();
            renderApp = new R_App();

            // Basic Setup
            DontDestroyOnLoad(gameObject);
            isInit = false;
            isTearDown = false;
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 120;

            // Camera
            var mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
            var uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();

            // Controller
            loginController = new LoginController();
            gameController = new GameController();

            // Inject
            infraContext.Inject(assetCore, templateCore, eventCore);
            uiApp.Inject(canvas, uiCamera, infraContext);
            logicApp.Inject(infraContext, renderApp.SetterAPI, uiApp.SetterAPI);
            renderApp.Inject(mainCamera, infraContext);

            loginController.Inject(infraContext, uiApp);
            gameController.Inject(infraContext, uiApp, logicApp, renderApp);

            // Binding Event
            BindingEvent();

            Action main = async () => {

                await infraContext.Init();
                loginController.Init();
                gameController.Init();
                isInit = true;
                OshiLog.Log("Init Done");

            };

            main.Invoke();

        }

        void BindingEvent() {

            eventCore.OnLoginHandle += () => {
                loginController.Exit();
                gameController.Enter();
                OshiLog.Log("OnLoginHandle");
            };

        }

        void Update() {

            if (!isInit) {
                return;
            }

            var dt = Time.deltaTime;

            gameController.Tick(dt);

        }

        void OnDestroy() {
            TearDown();
        }

        void OnApplicationQuit() {
            TearDown();
        }

        void TearDown() {

            if (isTearDown) {
                return;
            }

            isTearDown = true;
            gameController.TearDown();
            loginController.TearDown();

            logicApp?.TearDown();
            renderApp?.TearDown();
            uiApp?.TearDown();

            eventCore?.ClearAll();
            templateCore?.ClearAll();
            assetCore?.ClearAll();

            OshiLog.OnLog = null;
            OshiLog.OnWarning = null;
            OshiLog.OnError = null;
            OshiLog.OnAssert = null;
            OshiLog.OnAssertWithoutMessage = null;

            GC.Collect();

        }

    }

}
