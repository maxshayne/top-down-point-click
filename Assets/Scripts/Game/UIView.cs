using Game.PlayerInput;
using Infrastructure.DataStorage;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private Button m_ExitButton;
        
        private PlayerInputService _playerInputService;
        private IDataStorage<SaveData> _dataStorage;

        [Inject]
        public void Construct(PlayerInputService playerInputService, IDataStorage<SaveData> dataStorage)
        {
            _dataStorage = dataStorage;
            _playerInputService = playerInputService;
            m_ExitButton.onClick.AddListener(OnExit);
        }

        private void OnExit()
        {
            var saveData = _playerInputService.SavePlayerState();
            _dataStorage.Save(saveData);
            
#if UNITY_EDITOR
           // UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}