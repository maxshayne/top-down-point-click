using Game.PlayerInput;
using VContainer.Unity;
namespace Game
{
    public class GameEntry : IStartable
    {
        private readonly PlayerInputService _playerInputService;

        public GameEntry(PlayerInputService playerInputService)
        {
            _playerInputService = playerInputService;
        }
        
        public void Start()
        {
        }
    }
}