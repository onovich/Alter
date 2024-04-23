using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Alter {

    public class TemplateInfraContext {

        GameConfig config;
        public AsyncOperationHandle configHandle;

        Dictionary<int, MapTM> mapDict;
        public AsyncOperationHandle mapHandle;

        Dictionary<int, BlockTM> blockDict;
        List<BlockTM> blockList;
        public AsyncOperationHandle blockHandle;

        public TemplateInfraContext() {
            mapDict = new Dictionary<int, MapTM>();
            blockDict = new Dictionary<int, BlockTM>();
            blockList = new List<BlockTM>();
        }

        // Game
        public void Config_Set(GameConfig config) {
            this.config = config;
        }

        public GameConfig Config_Get() {
            return config;
        }

        // Map
        public void Map_Add(MapTM map) {
            mapDict.Add(map.typeID, map);
        }

        public bool Map_TryGet(int typeID, out MapTM map) {
            var has = mapDict.TryGetValue(typeID, out map);
            if (!has) {
                GLog.LogError($"Map {typeID} not found");
            }
            return has;
        }

        // Block
        public void Block_Add(BlockTM block) {
            blockDict.Add(block.typeID, block);
            blockList.Add(block);
        }

        public bool Block_TryGet(int typeID, out BlockTM block) {
            var has = blockDict.TryGetValue(typeID, out block);
            if (!has) {
                GLog.LogError($"Block {typeID} not found");
            }
            return has;
        }

        public BlockTM Block_GetRandom(RandomService rd) {
            var blockTypeCount = blockDict.Count;
            var index = rd.NextIntRange(0, blockTypeCount);
            var block = blockList[index];
            return block;
        }

        // Clear
        public void Clear() {
            mapDict.Clear();
            blockDict.Clear();
        }

    }

}