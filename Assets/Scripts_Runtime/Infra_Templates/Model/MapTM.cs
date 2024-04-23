using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Alter {

    [CreateAssetMenu(fileName = "TM_Map", menuName = "Alter/TM/Map")]
    public class MapTM : ScriptableObject {

        public int typeID;
        public Vector2Int mapSize;
        public Vector2Int spawnPoint;

    }

}