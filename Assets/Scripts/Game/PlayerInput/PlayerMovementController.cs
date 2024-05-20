using System;
using EventBusSystem;
using Game.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.PlayerInput
{
    [UsedImplicitly]
    public class PlayerMovementController : IInputListener,  IWaypointReachHandler, IDisposable
    {
        public PlayerMovementController(
            IPathProvider pathProvider,
            IPlayerInput playerInput, 
            IPlayerMovement playerMovement)
        {
            _pathProvider = pathProvider;
            _playerMovement = playerMovement;
            _playerInput = playerInput;

            playerInput.Subscribe(this);
            EventBus.Subscribe(this);
        }
        
        public void Configure(SaveData saveData)
        {
            RestoreSaveState(saveData);
            if (!_playerMovement.IsMoving()) return;
            var pos = _playerMovement.CurrentTarget;
            _playerMovement.CreateDestination(pos);
        }
        
        public void NotifyPoint(Vector3 position)
        {
            _pathProvider.AddPointToPath(position);
            if (IsCharacterMoving()) return;
            SetNewDestinationFromQueue();
        }
        
        public void WaypointReached()
        {
            _playerMovement.ReachDestination();
            SetNewDestinationFromQueue();
        }
        
        public void Dispose()
        {
            _playerInput.Unsubscribe(this);
            EventBus.Unsubscribe(this);
        }

        private void RestoreSaveState(SaveData saveData)
        {
            if (saveData == null) return;
            _playerMovement.RestoreData(saveData);
            saveData.GetPoints().ForEach(_pathProvider.AddPointToPath);
        }
        
        private bool IsCharacterMoving() => _playerMovement.IsMoving();

        private void SetNewDestinationFromQueue()
        {
            if (!_pathProvider.TryGetNextPoint(out var pos)) return;
            _playerMovement.CreateDestination(pos);
        }

        private readonly IPathProvider _pathProvider;
        private readonly IPlayerMovement _playerMovement;
        private readonly IPlayerInput _playerInput;
    }
}