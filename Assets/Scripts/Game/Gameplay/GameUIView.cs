using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Gameplay
{
    public class GameUIView : MonoBehaviour
    {
        [SerializeField] private Button exitButton;
        
        private GamePresenter _presenter;
        
        [Inject]
        private void Construct(GamePresenter presenter)
        {
            _presenter = presenter;
        }
        
        private void Awake()
        {
            exitButton.onClick.AddListener(OnExit);
        }
        
        private void OnExit()
        {
            _presenter.ExitScene();
        }

        private void OnDestroy()
        {
            exitButton.onClick.RemoveAllListeners();
        }
    }
}