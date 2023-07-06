using System.Collections;
using System.Collections.Generic;
using Bill.Infra;
using UnityEngine;

namespace Oshi.Render {

    public class R_Context {

        // Camera
        Camera mainCamera;
        public Camera MainCamera => mainCamera;

        // WorldRoot
        Transform worldRoot;
        public Transform WorldRoot => worldRoot;

        // Map
        R_MapEntity mapEntity;
        public R_MapEntity MapEntity => mapEntity;

        // Repo
        R_RoleRepo roleRepo;
        public R_RoleRepo RoleRepo => roleRepo;

        R_PropRepo propRepo;
        public R_PropRepo PropRepo => propRepo;

        R_SolidRepo solidRepo;
        public R_SolidRepo SolidRepo => solidRepo;

        R_AnchorRepo anchorRepo;
        public R_AnchorRepo AnchorRepo => anchorRepo;

        // Domain
        R_AllDomain allDomain;
        public R_AllDomain AllDomain => allDomain;

        public R_Context() {
            allDomain = new R_AllDomain();
            roleRepo = new R_RoleRepo();
            propRepo = new R_PropRepo();
            solidRepo = new R_SolidRepo();
            anchorRepo = new R_AnchorRepo();
        }

        public void Inject(Camera mainCamera,
                           InfraContext infraContext) {
            this.mainCamera = mainCamera;
            this.allDomain.Inject(this);
        }

    }

}