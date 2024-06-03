using Cysharp.Threading.Tasks;
using Game.Root.Configuration;
using Infrastructure.AssetManagement;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay
{
    [UsedImplicitly]
    public class GameLevelConfigurator
    {
        private readonly IAssetProvider _assetProvider;
        private readonly LevelConfiguration _levelConfiguration;
        
        public GameLevelConfigurator(IAssetProvider assetProvider, LevelConfiguration levelConfiguration)
        {
            _assetProvider = assetProvider;
            _levelConfiguration = levelConfiguration;
        }

        public async UniTask LoadLevel()
        {
            var levelAsset = await _assetProvider.LoadAsset<GameObject>(_levelConfiguration.LevelAsset.AssetGUID);
            Object.Instantiate(levelAsset, null);
        }
    }
}