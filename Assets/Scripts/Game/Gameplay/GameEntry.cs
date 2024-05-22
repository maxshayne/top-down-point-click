using Cysharp.Threading.Tasks;
using Game.Data;
using Game.PlayerMovement;
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
            GamePresenter gamePresenter,
            GameLevelConfigurator gameLevelConfigurator,
            PlayerLevelConfigurator playerLevelConfigurator,
            ClickMovementController clickMovementController)
        {
            _sceneLoader = sceneLoader;
            _dataStorage = dataStorage;
            _gamePresenter = gamePresenter;
            _gameLevelConfigurator = gameLevelConfigurator;
            _playerLevelConfigurator = playerLevelConfigurator;
            _clickMovementController = clickMovementController;
        }

        public async void Start()
        {
            var saveData = await _dataStorage.Load();
            await UniTask.WhenAll(
                UniTask.WaitUntil(() => _sceneLoader.IsSceneLoaded),
                _gameLevelConfigurator.LoadLevel());
            _gamePresenter.Configure(saveData);
            _playerLevelConfigurator.Configure(saveData);
            _clickMovementController.Configure();
        }

        private readonly ClickMovementController _clickMovementController;
        private readonly ISceneLoader _sceneLoader;
        private readonly IDataStorage<SaveData> _dataStorage;
        private readonly GamePresenter _gamePresenter;
        private readonly GameLevelConfigurator _gameLevelConfigurator;
        private readonly PlayerLevelConfigurator _playerLevelConfigurator;
    }
}