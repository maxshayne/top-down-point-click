using Cysharp.Threading.Tasks;
using Game.Data;
using Game.PlayerInput;
using Game.Root;
using Infrastructure.DataStorage;
using JetBrains.Annotations;
using VContainer.Unity;

namespace Game.Gameplay
{
    [UsedImplicitly]
    public class GameEntry : IStartable
    {
        public GameEntry(
            ISceneLoader sceneLoader,
            IDataStorage<SaveData> dataStorage,
            DataBuilder<SaveData> saveDataBuilder,
            GameLevelConfigurator gameLevelConfigurator,
            PlayerLevelConfigurator playerLevelConfigurator,
            PlayerInputService playerInputService,
            IPlayerMovement playerMovement,
            GameUIView uiView)
        {
            _sceneLoader = sceneLoader;
            _dataStorage = dataStorage;
            _saveDataBuilder = saveDataBuilder;
            _gameLevelConfigurator = gameLevelConfigurator;
            _playerLevelConfigurator = playerLevelConfigurator;
            _playerInputService = playerInputService;
            _playerMovement = playerMovement;

            uiView.SetCallback(OnExitCallback);
        }

        public async void Start()
        {
            var save = await _dataStorage.Load();
            await UniTask.WhenAll(
                UniTask.WaitUntil(() => _sceneLoader.IsSceneLoaded),
                _gameLevelConfigurator.LoadLevel());
            _playerLevelConfigurator.Configure();
            _playerInputService.Configure(save);
        }

        private async void OnExitCallback()
        {
            var saveData = _saveDataBuilder
                .CreateEmptyState()
                .UpdateState(_playerMovement)
                .UpdateState(_playerInputService)
                .Build();
            await _dataStorage.Save(saveData);
            _sceneLoader.LoadScene(SceneKey.Menu);
        }

        private readonly PlayerInputService _playerInputService;
        private readonly IPlayerMovement _playerMovement;
        private readonly ISceneLoader _sceneLoader;
        private readonly IDataStorage<SaveData> _dataStorage;
        private readonly DataBuilder<SaveData> _saveDataBuilder;
        private readonly GameLevelConfigurator _gameLevelConfigurator;
        private readonly PlayerLevelConfigurator _playerLevelConfigurator;
    }
}