using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Alter {

    public static class TemplateInfra {

        public static async Task LoadAssets(TemplateInfraContext ctx) {

            {
                var handle = Addressables.LoadAssetAsync<GameConfig>("TM_Config");
                var cotmfig = await handle.Task;
                ctx.Config_Set(cotmfig);
                ctx.configHandle = handle;
            }

            {
                var handle = Addressables.LoadAssetsAsync<MapTM>("TM_Map", null);
                var mapList = await handle.Task;
                foreach (var tm in mapList) {
                    ctx.Map_Add(tm);
                }
                ctx.mapHandle = handle;
            }

            {
                var handle = Addressables.LoadAssetsAsync<BlockTM>("TM_Block", null);
                var blockList = await handle.Task;
                foreach (var tm in blockList) {
                    ctx.Block_Add(tm);
                }
                ctx.blockHandle = handle;
            }

            {
                var handle = Addressables.LoadAssetAsync<ColorTM>("TM_Color");
                var colorTM = await handle.Task;
                ctx.Color_Set(colorTM);
                ctx.colorHandle = handle;
            }

        }

        public static void Release(TemplateInfraContext ctx) {
            if (ctx.configHandle.IsValid()) {
                Addressables.Release(ctx.configHandle);
            }
            if (ctx.mapHandle.IsValid()) {
                Addressables.Release(ctx.mapHandle);
            }
            if (ctx.blockHandle.IsValid()) {
                Addressables.Release(ctx.blockHandle);
            }
            if (ctx.colorHandle.IsValid()) {
                Addressables.Release(ctx.colorHandle);
            }
        }

    }

}