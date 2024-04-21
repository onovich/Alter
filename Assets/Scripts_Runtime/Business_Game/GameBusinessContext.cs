using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alter {

    public class GameBusinessContext {

        // Entity
        public GameEntity gameEntity;
        public PlayerEntity playerEntity;
        public InputEntity inputEntity; // External
        public MapEntity currentMapEntity;

        public CellRepository cellRepo;

        // App
        public UIAppContext uiContext;
        public VFXAppContext vfxContext;

        // Camera
        public Camera mainCamera;

        // Service
        public IDRecordService idRecordService;

        // Infra
        public TemplateInfraContext templateInfraContext;
        public AssetsInfraContext assetsInfraContext;

        // Timer
        public float fixedRestSec;

        // TEMP
        public RaycastHit2D[] hitResults;

        public GameBusinessContext() {
            gameEntity = new GameEntity();
            playerEntity = new PlayerEntity();
            idRecordService = new IDRecordService();
            cellRepo = new CellRepository();
            hitResults = new RaycastHit2D[100];
        }

        public void Reset() {
            idRecordService.Reset();
            cellRepo.Clear();
        }

        // Block
        public void Cell_ForEach(Action<CellSubEntity> onAction) {
            cellRepo.ForEach(onAction);
        }

    }

}