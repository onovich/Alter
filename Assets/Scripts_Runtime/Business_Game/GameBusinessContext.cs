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
        public BlockEntity currentBlock;
        public BlockEntity previewBlock;

        // App
        public UIAppContext uiContext;
        public VFXAppContext vfxContext;

        // Camera
        public Camera mainCamera;

        // Transform
        public Transform cellBoard;

        // Next Block
        public int nextBlockTypeID;

        // Service
        public IDRecordService idRecordService;
        public RandomService randomService;

        // Infra
        public TemplateInfraContext templateInfraContext;
        public AssetsInfraContext assetsInfraContext;

        // Timer
        public float fixedRestSec;

        // TEMP
        public RaycastHit2D[] hitResults;

        public GameBusinessContext(Transform cellBoard) {
            gameEntity = new GameEntity();
            playerEntity = new PlayerEntity();
            idRecordService = new IDRecordService();
            randomService = new RandomService();
            cellRepo = new CellRepository();
            hitResults = new RaycastHit2D[100];
            this.cellBoard = cellBoard;
            nextBlockTypeID = -1;
        }

        public void Reset() {
            idRecordService.Reset();
            cellRepo.Clear();
        }

        public void SetCurrentBlock(BlockEntity block) {
            currentBlock = block;
        }

        public void SetPreviewBlock(BlockEntity block) {
            previewBlock = block;
        }

    }

}