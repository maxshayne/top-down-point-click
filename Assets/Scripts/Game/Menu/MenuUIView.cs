using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Menu
{
    public class MenuUIView : MonoBehaviour
    {
        [Inject]
        private void Construct(MenuPresenter presenter)
        {
            _presenter = presenter;
        }

        private void Awake()
        {
            m_StartButton.onClick.AddListener(Load);
            m_ClearSaveButton.onClick.AddListener(OnClearSave);
        }

        private void OnClearSave()
        {
            _presenter.ClearSave();
        }

        private void Load()
        {
            _presenter.LoadGame();
        }

        private void OnDestroy()
        {
            m_StartButton.onClick.RemoveAllListeners();
            m_ClearSaveButton.onClick.RemoveAllListeners();
        }

        [SerializeField] private Button m_StartButton;
        [SerializeField] private Button m_ClearSaveButton;
        
        private MenuPresenter _presenter;
    }
}