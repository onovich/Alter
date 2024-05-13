using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;

namespace Alter {

    public class Main : MonoBehaviour {

        [SerializeField] bool drawBlockSizeGizmos;

        InputEntity inputEntity;

        AssetsInfraContext assetsInfraContext;
        TemplateInfraContext templateInfraContext;

        LoginBusinessContext loginBusinessContext;
        GameBusinessContext gameBusinessContext;

        UIAppContext uiAppContext;
        VFXAppContext vfxAppContext;

        bool isLoadedAssets;
        bool isTearDown;

        void Start() {

            isLoadedAssets = false;
            isTearDown = false;

            Canvas mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
            Transform hudFakeCanvas = GameObject.Find("HUDFakeCanvas").transform;
            Camera mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
            Transform vfxRoot = GameObject.Find("VFXRoot").transform;
            Transform cellBoard = GameObject.Find("CellBoard").transform;

            inputEntity = new InputEntity();

            loginBusinessContext = new LoginBusinessContext();
            gameBusinessContext = new GameBusinessContext(cellBoard);

            uiAppContext = new UIAppContext("UI", mainCanvas, hudFakeCanvas, mainCamera);
            vfxAppContext = new VFXAppContext("VFX", vfxRoot);

            assetsInfraContext = new AssetsInfraContext();
            templateInfraContext = new TemplateInfraContext();

            // Inject
            loginBusinessContext.uiContext = uiAppContext;

            gameBusinessContext.inputEntity = inputEntity;
            gameBusinessContext.assetsInfraContext = assetsInfraContext;
            gameBusinessContext.templateInfraContext = templateInfraContext;
            gameBusinessContext.uiContext = uiAppContext;
            gameBusinessContext.vfxContext = vfxAppContext;
            gameBusinessContext.mainCamera = mainCamera;

            // TODO Camera

            // Binding
            Binding();

            Action action = async () => {
                try {
                    await LoadAssets();
                    Init();
                    Enter();
                    isLoadedAssets = true;
                } catch (Exception e) {
                    GLog.LogError(e.ToString());
                }
            };
            action.Invoke();

        }

        void Enter() {
            LoginBusiness.Enter(loginBusinessContext);
        }

        void Update() {

            if (!isLoadedAssets) {
                return;
            }

            var dt = Time.deltaTime;
            LoginBusiness.Tick(loginBusinessContext, dt);
            GameBusiness.Tick(gameBusinessContext, dt);

            UIApp.LateTick(uiAppContext, dt);

        }

        void Init() {

            Application.targetFrameRate = 120;

            var inputEntity = this.inputEntity;
            inputEntity.Ctor();
            inputEntity.Keybinding_Set(InputKeyEnum.MoveLeft, new KeyCode[] { KeyCode.A, KeyCode.LeftArrow });
            inputEntity.Keybinding_Set(InputKeyEnum.MoveRight, new KeyCode[] { KeyCode.D, KeyCode.RightArrow });
            inputEntity.Keybinding_Set(InputKeyEnum.MoveUp, new KeyCode[] { KeyCode.W, KeyCode.UpArrow });
            inputEntity.Keybinding_Set(InputKeyEnum.MoveDown, new KeyCode[] { KeyCode.S, KeyCode.DownArrow });
            inputEntity.Keybinding_Set(InputKeyEnum.Hold, new KeyCode[] { KeyCode.Space });

            GameBusiness.Init(gameBusinessContext);

            UIApp.Init(uiAppContext);
            VFXApp.Init(vfxAppContext);

        }

        void Binding() {
            var uiEvt = uiAppContext.evt;

            // UI
            // - Login
            uiEvt.Login_OnStartGameClickHandle += () => {
                LoginBusiness.Exit(loginBusinessContext);
                GameBusiness.StartGame(gameBusinessContext);
            };

            uiEvt.Login_OnExitGameClickHandle += () => {
                LoginBusiness.ExitApplication(loginBusinessContext);
            };

            // - GameInfo
            uiEvt.GameInfo_OnRestartBtnClickHandle += () => {
                GameBusiness.UIGameInfo_OnRestartBtnClick(gameBusinessContext);
            };

            // - GameOver
            uiEvt.GameOver_OnRestartGameClickHandle += () => {
                GameBusiness.UIGameOver_OnRestartGame(gameBusinessContext);
            };

            uiEvt.GameOver_OnExitGameClickHandle += () => {
                GameBusiness.UIGameOver_OnExitGameClick(gameBusinessContext);
            };

        }

        async Task LoadAssets() {
            await UIApp.LoadAssets(uiAppContext);
            await VFXApp.LoadAssets(vfxAppContext);
            await AssetsInfra.LoadAssets(assetsInfraContext);
            await TemplateInfra.LoadAssets(templateInfraContext);
        }

        void OnApplicationQuit() {
            TearDown();
        }

        void OnDestroy() {
            TearDown();
        }

        void TearDown() {
            if (isTearDown) {
                return;
            }
            isTearDown = true;

            GameBusiness.TearDown(gameBusinessContext);

            AssetsInfra.ReleaseAssets(assetsInfraContext);
            TemplateInfra.Release(templateInfraContext);

            loginBusinessContext.Clear();
            uiAppContext.Clear();

            templateInfraContext.Clear();
            assetsInfraContext.Clear();
        }

        void OnDrawGizmos() {
            GameBusiness.OnDrawGizmos(gameBusinessContext, drawBlockSizeGizmos);
        }

    }

}