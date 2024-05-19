namespace Game.Root
{
    public interface ISceneLoader
    {
        bool IsSceneLoaded { get; }
        void LoadScene(SceneKey scene);
    }
}