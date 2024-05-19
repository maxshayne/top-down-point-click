using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Root.SceneManagement
{
    [UsedImplicitly]
    public class SceneLoader : ISceneLoader
    {
        public SceneLoader(ConfigurationData configurationData)
        {
            _configurationData = configurationData;
            Configure();
        }

        public bool IsSceneLoaded { get; private set; }

        public async void LoadScene(SceneKey scene)
        {
            if (!_sceneMap.TryGetValue(scene, out var path))
            {
                Debug.LogError($"Can't load scene with key [{scene}]! SceneMap doesn't contains this path");
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
        
        private void Configure()
        {
            Debug.LogError($"Configure");

            var cg = _configurationData.LoadingScreen.LoadAssetAsync<GameObject>().WaitForCompletion();
            _loadingCanvasGroup = Object.Instantiate(cg.GetComponent<CanvasGroup>(), null);
            _loadingCanvasGroup.alpha = 0;
            Object.DontDestroyOnLoad(_loadingCanvasGroup);
        }

        private readonly Dictionary<SceneKey, string> _sceneMap = new ()
        {
            {SceneKey.Menu, "Assets/Scenes/Menu.unity"},
            {SceneKey.Game, "Assets/Scenes/Game.unity"}
        };

        private const int FakeLoadingDelayInMilliseconds = 3000;
        
        private readonly ConfigurationData _configurationData;
        private CanvasGroup _loadingCanvasGroup;
    }
}