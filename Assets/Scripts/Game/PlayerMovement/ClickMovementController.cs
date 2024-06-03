using System;
using EventBusSystem;
using Game.PlayerInput;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.PlayerMovement
{
    [UsedImplicitly]
    public class ClickMovementController : IInputListener,  IWaypointReachHandler, IDisposable
    {
        private readonly IInputValidator _inputValidator;
        private readonly IPathProvider _pathProvider;
        private readonly IPlayerMovement _playerMovement;
        private readonly IPlayerInput _playerInput;
        
        public ClickMovementController(
            IInputValidator inputValidator,
            IPathProvider pathProvider,
            IPlayerInput playerInput, 
            IPlayerMovement playerMovement)
        {
            _inputValidator = inputValidator;
            _pathProvider = pathProvider;
            _playerMovement = playerMovement;
            _playerInput = playerInput;

            playerInput.Subscribe(this);
            EventBus.Subscribe(this);
        }
        
        public void Initialize()
        {
            SetNewDestinationFromQueue();
        }

        public void NotifyInput(Vector3 clickPosition)
        {
            if (!_inputValidator.TryValidateClick(clickPosition, out var pos)) return;
            _pathProvider.AddPointToPath(pos);
            if (_playerMovement.IsMoving()) return;
            SetNewDestinationFromQueue();
        }

        public void WaypointReached()
        {
            _pathProvider.RemovePoint();
            _playerMovement.ReachDestination();
            SetNewDestinationFromQueue();
        }
        
        public void Dispose()
        {
            _playerInput.Unsubscribe(this);
            EventBus.Unsubscribe(this);
        }

        private void SetNewDestinationFromQueue()
        {
            if (!_pathProvider.TryPeekNextPoint(out var pos)) return;
            _playerMovement.CreateDestination(pos);
        }
    }
}