using EventBusSystem;
using Game.Root;
using Infrastructure.DataStorage;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Menu
{
    public class MenuUIView : MonoBehaviour
    {
        [SerializeField] private Button m_StartButton;
        [SerializeField] private Button m_ClearSaveButton;
        
        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Awake()
        {
            m_StartButton.onClick.AddListener(Load);
            m_ClearSaveButton.onClick.AddListener(OnClearSave);
        }

        private void OnClearSave()
        {
            EventBus.RaiseEvent<IDataStorageEventHandler>(h=>h.ClearStorage());
        }

        private void Load()
        {
            _sceneLoader.LoadScene(SceneKey.Game);
        }

        private ISceneLoader _sceneLoader;
    }
}