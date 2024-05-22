using Game.Data;
using Game.PlayerMovement;
using Game.Root;
using Infrastructure.DataStorage;
using JetBrains.Annotations;

namespace Game.Gameplay
{
    [UsedImplicitly]
    public class GamePresenter
    {
        public GamePresenter(
            ISceneLoader sceneLoader,
            IDataStorage<SaveData> dataStorage,
            DataBuilder<SaveData> saveDataBuilder,
            IPathProvider pathProvider,
            IPlayerMovement playerMovement,
            PlayerLevelConfigurator playerLevelConfigurator)
        {
            _sceneLoader = sceneLoader;
            _dataStorage = dataStorage;
            _saveDataBuilder = saveDataBuilder;
            _pathProvider = pathProvider;
            _playerLevelConfigurator = playerLevelConfigurator;
        }
        
        public void Configure(SaveData saveData)
        {
            saveData?.GetPoints().ForEach(_pathProvider.AddPointToPath);
        }

        public async void ExitScene()
        {
            var saveData = GetLatestGameState();
            await _dataStorage.Save(saveData);
            _sceneLoader.LoadScene(SceneKey.Menu);
        }
        
        private SaveData GetLatestGameState()
        {
            var saveData = _saveDataBuilder
                .CreateEmptyState()
                .UpdateState(_playerLevelConfigurator)
                .UpdateState(_pathProvider)
                .Build();
            return saveData;
        }

        private readonly ISceneLoader _sceneLoader;
        private readonly IDataStorage<SaveData> _dataStorage;
        private readonly DataBuilder<SaveData> _saveDataBuilder;
        private readonly IPathProvider _pathProvider;
        private readonly PlayerLevelConfigurator _playerLevelConfigurator;
    }
}