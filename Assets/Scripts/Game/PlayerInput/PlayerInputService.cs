using System.Collections.Generic;
using System.Linq;
using Game.Data;
using Game.Root.Configuration;
using Infrastructure.DataStorage;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.PlayerInput
{
    [UsedImplicitly]
    public class PlayerInputService : IInputListener, IBuilderAgent<SaveData>
    {
        public PlayerInputService(
            InputConfiguration inputConfiguration,
            IPlayerInput playerInput, 
            IPlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
            _inputConfiguration = inputConfiguration;
            
            playerInput.Subscribe(this);
        }
        
        public void Configure(SaveData saveData)
        {
            RestorePlayerState(saveData);
            StartMoveIfPossible();
        }
        
        public void NotifyPoint(Vector3 position)
        {
            AddPointToQueue(position);
            if (IsCharacterMoving()) return;
            SetNewDestinationFromQueue();
        }
        
        public SaveData UpdateState(SaveData state)
        {
            state.Points = _pathQueue.ToList();
            return state;
        }
        
        private void StartMoveIfPossible()
        {
            var pos = _playerMovement.CurrentTarget;
            if (!pos.HasValue) return;
            _playerMovement.CreateDestination(pos.Value);
            CreateWaypoint(pos.Value);
        }

        private void RestorePlayerState(SaveData saveData)
        {
            if (saveData == null) return;
            _playerMovement.SetTransformValues(saveData);
            saveData.Points.ForEach(AddPointToQueue);
        }
        
        private void AddPointToQueue(Vector3 position)
        {
            if (_pathQueue.Count >= _inputConfiguration.MaxPointsQueue) return;
            _pathQueue.Enqueue(position);
            CreateWaypoint(position);
        }

        private void CreateWaypoint(Vector3 newPos)
        {
            Debug.LogWarning($"create waypoint in {newPos}");
            var go = new GameObject().AddComponent<WaypointChecker>();
            go.Configure(newPos, OnPointReached, PlayerTag);
        }

        private bool IsCharacterMoving() => _playerMovement.IsMoving();

        private void SetNewDestinationFromQueue()
        {
            if (!_pathQueue.TryDequeue(out var pos))
            {
                Debug.LogWarning($"cant get next point, queue count is {_pathQueue.Count}");
                return;
            }
            _playerMovement.CreateDestination(pos);
        }

        private void OnPointReached()
        {
            _playerMovement.ReachDestination();
            SetNewDestinationFromQueue();
        }
        
        private const string PlayerTag = "Player";

        private readonly IPlayerMovement _playerMovement;
        private readonly InputConfiguration _inputConfiguration;
        private readonly Queue<Vector3> _pathQueue = new();
    }
}