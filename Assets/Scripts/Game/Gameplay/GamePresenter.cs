using Game.Data;
using Infrastructure.DataStorage;
using Infrastructure.SceneManagement;
using JetBrains.Annotations;

namespace Game.Gameplay
{
    [UsedImplicitly]
    public class GamePresenter
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IDataStorage<SaveData> _dataStorage;
        private readonly SaveLoadPresenter _saveLoadPresenter;
        
        public GamePresenter(
            ISceneLoader sceneLoader,
            IDataStorage<SaveData> dataStorage,
            SaveLoadPresenter saveLoadPresenter)
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
    }
}