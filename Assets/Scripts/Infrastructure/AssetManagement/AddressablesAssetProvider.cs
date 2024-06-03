using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Infrastructure.AssetManagement
{
    public class AddressablesAssetProvider : IAssetProvider
    {
        public async UniTask<T> LoadAsset<T>(string assetPath)
        {
            var handle = await Addressables.LoadAssetAsync<T>(assetPath);
            return handle;
        }
    }
}