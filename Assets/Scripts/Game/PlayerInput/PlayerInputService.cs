using System;
using System.Collections.Generic;
using System.Linq;
using EventBusSystem;
using Game.Data;
using Game.Root.Configuration;
using Infrastructure.DataStorage;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.PlayerInput
{
    [UsedImplicitly]
    public class PlayerInputService : IInputListener, IBuilderAgent<SaveData>, IWaypointReachHandler, IDisposable
    {
        public PlayerInputService(
            InputConfiguration inputConfiguration,
            IPlayerInput playerInput, 
            IPlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
            _inputConfiguration = inputConfiguration;
            _playerInput = playerInput;

            playerInput.Subscribe(this);
            EventBus.Subscribe(this);
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
        
        private void StartMoveIfPossible()
        {
            if (!_playerMovement.IsMoving()) return;
            var pos = _playerMovement.CurrentTarget;
            _playerMovement.CreateDestination(pos);
            CreateWaypoint(pos);
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
            go.Configure(newPos, PlayerTag);
        }

        private bool IsCharacterMoving() => _playerMovement.IsMoving();

        private void SetNewDestinationFromQueue()
        {
            if (!_pathQueue.TryDequeue(out var pos))
            {
                Debug.LogWarning($"cant get next point, queue count is {_pathQueue.Count}");
                return;
            }
            Debug.Log($" get next point{pos.ToString()}");
            _playerMovement.CreateDestination(pos);
        }
        
        private const string PlayerTag = "Player";

        private readonly IPlayerMovement _playerMovement;
        private readonly InputConfiguration _inputConfiguration;
        private readonly IPlayerInput _playerInput;
        private readonly Queue<Vector3> _pathQueue = new();
    }
}