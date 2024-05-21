using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Gameplay
{
    public class GameUIView : MonoBehaviour
    {
        [Inject]
        private void Construct(GamePresenter presenter)
        {
            _presenter = presenter;
        }
        
        private void Awake()
        {
            m_ExitButton.onClick.AddListener(OnExit);
        }
        
        private void OnExit()
        {
            _presenter.ExitScene();
        }

        private void OnDestroy()
        {
            m_ExitButton.onClick.RemoveAllListeners();
        }
        
        [SerializeField] private Button m_ExitButton;
        
        private GamePresenter _presenter;
    }
}