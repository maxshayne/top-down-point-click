using Game.Data;
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
            SaveLoadPresenter saveLoadPresenter,
            PlayerLevelConfigurator playerLevelConfigurator)
        {
            _sceneLoader = sceneLoader;
            _dataStorage = dataStorage;
            _saveLoadPresenter = saveLoadPresenter;
        }

        public async void ExitScene()
        {
            var saveData = _saveLoadPresenter.GetLatestSave();
            await _dataStorage.Save(saveData);
            _sceneLoader.LoadScene(SceneKey.Menu);
        }

        private readonly ISceneLoader _sceneLoader;
        private readonly IDataStorage<SaveData> _dataStorage;
        private readonly SaveLoadPresenter _saveLoadPresenter;
    }
}