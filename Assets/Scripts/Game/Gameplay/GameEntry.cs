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
            PlayerMovementController playerMovementController,
            IPathProvider pathProvider,
            IPlayerMovement playerMovement)
        {
            _sceneLoader = sceneLoader;
            _dataStorage = dataStorage;
            _gameLevelConfigurator = gameLevelConfigurator;
            _playerLevelConfigurator = playerLevelConfigurator;
            _playerMovementController = playerMovementController;
        }

        public async void Start()
        {
            var save = await _dataStorage.Load();
            await UniTask.WhenAll(
                UniTask.WaitUntil(() => _sceneLoader.IsSceneLoaded),
                _gameLevelConfigurator.LoadLevel());
            _playerLevelConfigurator.Configure();
            _playerMovementController.Configure(save);
        }

        private readonly PlayerMovementController _playerMovementController;
        private readonly ISceneLoader _sceneLoader;
        private readonly IDataStorage<SaveData> _dataStorage;
        private readonly GameLevelConfigurator _gameLevelConfigurator;
        private readonly PlayerLevelConfigurator _playerLevelConfigurator;
    }
}