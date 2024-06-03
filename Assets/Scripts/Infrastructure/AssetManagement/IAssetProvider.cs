using Cysharp.Threading.Tasks;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        UniTask<T> LoadAsset<T>(string assetPath);
    }
}