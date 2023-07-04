using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oshi.Template {

    public struct MapTM {

        public int id;
        public Vector2Int size;
        public Vector2 offset;
        public RoleSpawnTM[] roleArray;
        public PropSpawnTM[] propArray;
        public SolidSpawnTM[] solidArray;
        public AnchorSpawnTM[] anchorArray;
        public SolidSpawnTM[] solidSpawnArray;

    }

}