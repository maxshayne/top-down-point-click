using Game.Data;
using Game.PlayerInput;
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
            IPlayerMovement playerMovement)
        {
            _sceneLoader = sceneLoader;
            _dataStorage = dataStorage;
            _saveDataBuilder = saveDataBuilder;
            _pathProvider = pathProvider;
            _playerMovement = playerMovement;
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
                .UpdateState(_playerMovement)
                .UpdateState(_pathProvider)
                .Build();
            return saveData;
        }

        private readonly ISceneLoader _sceneLoader;
        private readonly IDataStorage<SaveData> _dataStorage;
        private readonly DataBuilder<SaveData> _saveDataBuilder;
        private readonly IPathProvider _pathProvider;
        private readonly IPlayerMovement _playerMovement;
    }
}