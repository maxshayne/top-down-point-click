using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay
{
    public class GameUIView : MonoBehaviour
    {
        public void SetCallback(Action onExitCallback)
        {
            m_ExitButton.onClick.AddListener(() => onExitCallback?.Invoke());
        }
        
        [SerializeField] private Button m_ExitButton;
    }
}