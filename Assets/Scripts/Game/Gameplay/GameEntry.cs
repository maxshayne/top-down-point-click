using Cysharp.Threading.Tasks;
using Game.Data;
using Game.PlayerMovement;
using Infrastructure.DataStorage;
using Infrastructure.SceneManagement;
using JetBrains.Annotations;
using VContainer.Unity;

namespace Game.Gameplay
{
    [UsedImplicitly]
    public class GameEntry : IStartable
    {
        private readonly ClickMovementController _clickMovementController;
        private readonly SaveLoadPresenter _saveLoadPresenter;
        private readonly ISceneLoader _sceneLoader;
        private readonly IDataStorage<SaveData> _dataStorage;
        private readonly GameLevelConfigurator _gameLevelConfigurator;
        private readonly PlayerLevelConfigurator _playerLevelConfigurator;
        
        public GameEntry(
            ISceneLoader sceneLoader,
            IDataStorage<SaveData> dataStorage,
            GameLevelConfigurator gameLevelConfigurator,
            PlayerLevelConfigurator playerLevelConfigurator,
            ClickMovementController clickMovementController,
            SaveLoadPresenter saveLoadPresenter)
        {
            _sceneLoader = sceneLoader;
            _dataStorage = dataStorage;
            _gameLevelConfigurator = gameLevelConfigurator;
            _playerLevelConfigurator = playerLevelConfigurator;
            _clickMovementController = clickMovementController;
            _saveLoadPresenter = saveLoadPresenter;
        }

        public async void Start()
        {
            var saveData = await _dataStorage.Load();
            await UniTask.WhenAll(
                UniTask.WaitUntil(() => _sceneLoader.IsSceneLoaded),
                _gameLevelConfigurator.LoadLevel());
            _saveLoadPresenter.Load(saveData);
            _playerLevelConfigurator.Configure();
            _clickMovementController.Initialize();
        }
    }
}