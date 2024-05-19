using Game.PlayerInput;
using Infrastructure.DataStorage.Interfaces;
using JetBrains.Annotations;
using VContainer.Unity;

namespace Game
{
    [UsedImplicitly]
    public class GameEntry : IStartable
    {
        private readonly PlayerInputService _playerInputService;
        private readonly IDataStorage<SaveData> _dataStorage;

        public GameEntry(IDataStorage<SaveData> dataStorage, PlayerInputService playerInputService)
        {
            _dataStorage = dataStorage;
            _playerInputService = playerInputService;
        }
        
        public void Start()
        {
            var save = _dataStorage.Load();
            _playerInputService.Configure(save);
        }
    }
}