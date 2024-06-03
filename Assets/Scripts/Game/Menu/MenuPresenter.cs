using Game.Data;
using Infrastructure.DataStorage;
using Infrastructure.SceneManagement;
using JetBrains.Annotations;

namespace Game.Menu
{
    [UsedImplicitly]
    public class MenuPresenter
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IDataStorage<SaveData> _dataStorage;
        
        public MenuPresenter(ISceneLoader sceneLoader, IDataStorage<SaveData> dataStorage)
        {
            _sceneLoader = sceneLoader;
            _dataStorage = dataStorage;
        }

        public void LoadGame()
        {
            _sceneLoader.LoadScene(SceneKey.Game);
        }

        public void ClearSave()
        {
            _dataStorage.Clear();
        }
    }
}