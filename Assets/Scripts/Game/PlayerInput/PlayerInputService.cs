using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Game.PlayerInput
{
    [UsedImplicitly]
    public class PlayerInputService 
    {
        public PlayerInputService(NavMeshAgent player, Camera worldCamera)
        {
            _player = player;
            _worldCamera = worldCamera;
            Configure();
        }
        
        private void Configure()
        {
            _controls = new Controls();
            _controls.Main.Move.performed += OnMovePerformed;
            _controls.Enable();
        }

        private void OnMovePerformed(InputAction.CallbackContext obj)
        {
            if (!TryGetHitPosition(out var newPosition)) return;
            _pathQueue.Enqueue(newPosition); 
            CreateWaypoint(newPosition);
            if (IsCharacterMoving()) return;
            SetNewDestination();
        }

        private bool TryGetHitPosition(out Vector3 pos)
        {
            pos = Vector3.zero;
            var ray = _worldCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return false;
            pos = hit.point;
            return !hit.collider.CompareTag(ObstacleTag);
        }

        private void CreateWaypoint(Vector3 newPos)
        {
            var go = new GameObject().AddComponent<WaypointChecker>();
            go.Configure(newPos, OnPointReached, _player.tag);
        }

        private bool IsCharacterMoving()
        {
            return _currentTarget.HasValue;
        }

        private void SetNewDestination()
        {
            if (!_pathQueue.TryDequeue(out var pos)) return;
            _player.SetDestination(pos);
            _currentTarget = pos;
        }

        private void OnPointReached()
        {
            _currentTarget = null;
            SetNewDestination();
        }
        
        private const string ObstacleTag = "Obstacle";
        
        private readonly Queue<Vector3> _pathQueue = new();
        private readonly NavMeshAgent _player;
        private readonly Camera _worldCamera;
        
        private Controls _controls;
        private Vector3? _currentTarget;
    }
}