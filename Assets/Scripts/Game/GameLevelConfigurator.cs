using Cysharp.Threading.Tasks;
using Game.Root.Configuration;
using UnityEngine;
using VContainer.Unity;

namespace Game
{
    public class GameLevelConfigurator : IInitializable
    {
        private readonly LevelConfiguration _levelConfiguration;

        public GameLevelConfigurator(LevelConfiguration levelConfiguration)
        {
            _levelConfiguration = levelConfiguration;
        }

        public async void Initialize()
        {
            var handle = await _levelConfiguration.LevelAsset.LoadAssetAsync<GameObject>();
            Object.Instantiate(handle, null);
        }
    }
}