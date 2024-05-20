using Game.Root.Configuration;
using JetBrains.Annotations;
using UnityEngine.AI;

namespace Game
{
    [UsedImplicitly]
    public class PlayerLevelConfigurator
    {
        public PlayerLevelConfigurator(NavMeshAgent playerAgent, PlayerConfiguration playerConfiguration)
        {
            _playerAgent = playerAgent;
            _playerConfiguration = playerConfiguration;
        }
        
        public void Configure()
        {
            _playerAgent.speed = _playerConfiguration.MoveSpeed;
            _playerAgent.angularSpeed = _playerConfiguration.RotationSpeed;
        }
        
        private readonly NavMeshAgent _playerAgent;
        private readonly PlayerConfiguration _playerConfiguration;
    }
}