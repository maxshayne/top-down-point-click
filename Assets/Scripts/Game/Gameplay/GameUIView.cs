using EventBusSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay
{
    public class GameUIView : MonoBehaviour
    {
        private void Awake()
        {
            m_ExitButton.onClick.AddListener(OnExit);
        }
        
        private void OnExit()
        {
            EventBus.RaiseEvent<IExitListener>(h => h.CallExit());
        }

        [SerializeField] private Button m_ExitButton;
    }
}