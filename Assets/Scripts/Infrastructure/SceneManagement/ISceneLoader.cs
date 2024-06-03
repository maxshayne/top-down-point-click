namespace Infrastructure.SceneManagement
{
    public interface ISceneLoader
    {
        bool IsSceneLoaded { get; }
        void LoadScene(SceneKey scene);
    }
}