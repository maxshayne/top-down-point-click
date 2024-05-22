using Game.Data;
using Game.Root.Configuration;
using Infrastructure.DataStorage;
using JetBrains.Annotations;
using UnityEngine.AI;

namespace Game.Gameplay
{
    [UsedImplicitly]
    public class PlayerLevelConfigurator : IBuilderAgent<SaveData>
    {
        public PlayerLevelConfigurator(
            NavMeshAgent playerAgent, 
            PlayerConfiguration playerConfiguration)
        {
            _playerAgent = playerAgent;
            _playerConfiguration = playerConfiguration;
        }
        
        public void Configure(SaveData data)
        {
            _playerAgent.speed = _playerConfiguration.MoveSpeed;
            _playerAgent.angularSpeed = _playerConfiguration.RotationSpeed;
            
            if (data == null) return;
            var tr = _playerAgent.transform;
            tr.localPosition = data.LocalPosition;
            tr.localEulerAngles  = data.LocalEulerRotation;
            tr.localScale  = data.LocalScale;
        }
        
        public SaveData UpdateState(SaveData state)
        {
            var transform = _playerAgent.transform;
            state.LocalPosition = transform.localPosition;
            state.LocalEulerRotation = transform.localEulerAngles;
            state.LocalScale = transform.localScale;
            return state;
        }
        
        private readonly NavMeshAgent _playerAgent;
        private readonly PlayerConfiguration _playerConfiguration;
    }
}