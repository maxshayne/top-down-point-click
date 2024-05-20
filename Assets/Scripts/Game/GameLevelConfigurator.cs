using Cysharp.Threading.Tasks;
using Game.Root.Configuration;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer.Unity;

namespace Game
{
    [UsedImplicitly]
    public class GameLevelConfigurator : IInitializable
    {
        public GameLevelConfigurator(LevelConfiguration levelConfiguration)
        {
            _levelConfiguration = levelConfiguration;
        }

        public async void Initialize()
        {
            var handle = await Addressables.LoadAssetAsync<GameObject>(_levelConfiguration.LevelAsset);
            Object.Instantiate(handle, null);
        }
        
        private readonly LevelConfiguration _levelConfiguration;
    }
}