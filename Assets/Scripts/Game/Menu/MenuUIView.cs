using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Menu
{
    public class MenuUIView : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button clearSaveButton;
        
        private MenuPresenter _presenter;
        
        [Inject]
        private void Construct(MenuPresenter presenter)
        {
            _presenter = presenter;
        }

        private void Awake()
        {
            startButton.onClick.AddListener(Load);
            clearSaveButton.onClick.AddListener(OnClearSave);
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
            startButton.onClick.RemoveAllListeners();
            clearSaveButton.onClick.RemoveAllListeners();
        }
    }
}