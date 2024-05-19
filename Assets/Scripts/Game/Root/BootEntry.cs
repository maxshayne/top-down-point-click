using VContainer.Unity;

namespace Game.Root
{
    public class BootEntry : IInitializable
    {
        private readonly ISceneLoader _sceneLoader;

        public BootEntry(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void Initialize()
        {
            _sceneLoader.LoadScene(SceneKey.Menu);
        }
    }
}