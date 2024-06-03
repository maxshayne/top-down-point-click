using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Root.Configuration;
using Infrastructure.AssetManagement;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.SceneManagement
{
    [UsedImplicitly]
    public class SceneLoader : ISceneLoader
    {
        private const int FakeLoadingDelayInMilliseconds = 3000;
        
        private readonly Dictionary<SceneKey, string> _sceneMap = new ()
        {
            {SceneKey.Menu, "Assets/Scenes/Menu.unity"},
            {SceneKey.Game, "Assets/Scenes/Game.unity"}
        };
        private readonly IAssetProvider _assetProvider;
        private readonly ConfigurationData _configurationData;
        
        private CanvasGroup _loadingCanvasGroup;
        
        public SceneLoader(IAssetProvider assetProvider, ConfigurationData configurationData)
        {
            _assetProvider = assetProvider;
            _configurationData = configurationData;
            Configure();
        }

        public bool IsSceneLoaded { get; private set; }

        public async void LoadScene(SceneKey scene)
        {
            await UniTask.WaitUntil(() => _loadingCanvasGroup != null);
            if (!_sceneMap.TryGetValue(scene, out var path))
            {
                Debug.LogError($"Can't load scene with key [{scene}]! SceneMap doesn't contains this path");
                return;
            }

            IsSceneLoaded = false;
            _loadingCanvasGroup.blocksRaycasts = true;
            _loadingCanvasGroup.alpha = 1;
            var operationHandle = Addressables.LoadSceneAsync(path);
            var delay=  UniTask.Delay(FakeLoadingDelayInMilliseconds);
            await UniTask.WhenAll(operationHandle.ToUniTask(), delay);
            _loadingCanvasGroup.alpha = 0;
            _loadingCanvasGroup.blocksRaycasts = false;
            IsSceneLoaded = true;
        }

        private async void Configure()
        {
            var cg = await _assetProvider.LoadAsset<GameObject>(_configurationData.LoadingScreen.AssetGUID);
            _loadingCanvasGroup = Object.Instantiate(cg.GetComponent<CanvasGroup>(), null);
            _loadingCanvasGroup.alpha = 0;
            Object.DontDestroyOnLoad(_loadingCanvasGroup);
        }
    }
}