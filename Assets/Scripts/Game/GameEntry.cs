using Cysharp.Threading.Tasks;
using Game.PlayerInput;
using Game.Root;
using Infrastructure.DataStorage;
using JetBrains.Annotations;
using VContainer.Unity;

namespace Game
{
    [UsedImplicitly]
    public class GameEntry : IStartable
    {
        public GameEntry(
            ISceneLoader sceneLoader,
            IDataStorage<SaveData> dataStorage,
            PlayerLevelConfigurator playerLevelConfigurator,
            PlayerInputService playerInputService,
            GameUIView uiView)
        {
            _sceneLoader = sceneLoader;
            _dataStorage = dataStorage;
            _playerLevelConfigurator = playerLevelConfigurator;
            _playerInputService = playerInputService;

            uiView.SetCallback(OnExitCallback);
        }

        public async void Start()
        {
            var save = await _dataStorage.Load();
            await UniTask.WaitUntil(() => _sceneLoader.IsSceneLoaded);
            _playerLevelConfigurator.Configure();
            _playerInputService.Configure(save);
        }
        
        private async void OnExitCallback()
        {
            var saveState = _playerInputService.SavePlayerState();
            await _dataStorage.Save(saveState);
            _sceneLoader.LoadScene(SceneKey.Menu);
        }

        private readonly PlayerInputService _playerInputService;
        private readonly ISceneLoader _sceneLoader;
        private readonly IDataStorage<SaveData> _dataStorage;
        private readonly PlayerLevelConfigurator _playerLevelConfigurator;
    }
}