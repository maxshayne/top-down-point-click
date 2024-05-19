using Game.Root;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Menu
{
    public class MenuUIView : MonoBehaviour
    {
        [SerializeField] private Button m_StartButton;
        
        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Awake()
        {
            m_StartButton.onClick.AddListener(Load);
        }

        private void Load()
        {
            _sceneLoader.LoadScene(SceneKey.Game);
        }

        private ISceneLoader _sceneLoader;
    }
}