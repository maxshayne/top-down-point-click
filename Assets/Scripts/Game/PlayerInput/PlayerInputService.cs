using System.Collections.Generic;
using System.Linq;
using Game.Root.Configuration;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Game.PlayerInput
{
    [UsedImplicitly]
    public class PlayerInputService
    {
        public PlayerInputService(NavMeshAgent player, Camera worldCamera, InputConfiguration inputConfiguration)
        {
            _player = player;
            _worldCamera = worldCamera;
            _inputConfiguration = inputConfiguration;
            _controls = new Controls();
        }
        
        public void Configure(SaveData saveData)
        {
            RestorePlayerState(saveData);
            _controls.Main.Move.started += OnMovePerformed;
            _controls.Enable();
            if (!_currentTarget.HasValue) return;
            CreateWaypoint(_currentTarget.Value);
            SetDestinationPoint(_currentTarget.Value);
        }
        
        public SaveData SavePlayerState()
        {
            var tr = _player.transform;
            var state = new SaveData
            {
                LastPoint = _currentTarget,
                LocalPosition = tr.localPosition,
                LocalEulerRotation = tr.localEulerAngles,
                LocalScale = tr.localScale,
                Points = _pathQueue.ToList()
            };
            return state;
        }

        private void RestorePlayerState(SaveData saveData)
        {
            if (saveData == null) return;
            var tr = _player.transform;
            tr.localPosition = saveData.LocalPosition;
            tr.localEulerAngles  = saveData.LocalEulerRotation;
            tr.localScale  = saveData.LocalScale;
            _currentTarget = saveData.LastPoint;
            saveData.Points.ForEach(AddPointToQueue);
        }

        private void OnMovePerformed(InputAction.CallbackContext obj)
        {
            if (!TryGetHitPosition(out var newPosition)) return;
            AddPointToQueue(newPosition);
            if (IsCharacterMoving()) return;
            SetNewDestinationFromQueue();
        }
        
        private void AddPointToQueue(Vector3 position)
        {
            if (_pathQueue.Count >= _inputConfiguration.MaxPointsQueue) return;
            _pathQueue.Enqueue(position);
            CreateWaypoint(position);
        }

        private bool TryGetHitPosition(out Vector3 pos)
        {
            pos = Vector3.zero;
            var ray = _worldCamera.ScreenPointToRay(Pointer.current.position.value);
            if (ClickedOnUI()) return false;
            if (!Physics.Raycast(ray, out var hit)) return false;
            pos = hit.point;
            if (hit.collider.CompareTag("Player")) return false;
            return !hit.collider.CompareTag(ObstacleTag);
        }

        private bool ClickedOnUI()
        {
            return false;
        }

        private void CreateWaypoint(Vector3 newPos)
        {
            Debug.LogWarning($"create waypoint in {newPos}");

            var go = new GameObject().AddComponent<WaypointChecker>();
            go.Configure(newPos, OnPointReached, _player.tag);
        }

        private bool IsCharacterMoving() => _currentTarget.HasValue;

        private void SetNewDestinationFromQueue()
        {
            if (!_pathQueue.TryDequeue(out var pos))
            {
                Debug.LogWarning($"cant get next point, queue count is {_pathQueue.Count}");
                return;
            }
            SetDestinationPoint(pos);
        }

        private void SetDestinationPoint(Vector3 pos)
        {
            _player.SetDestination(pos);
            _currentTarget = pos;
        }

        private void OnPointReached()
        {
            _currentTarget = null;
            SetNewDestinationFromQueue();
        }
        
        private const string ObstacleTag = "Obstacle";
        
        private readonly NavMeshAgent _player;
        private readonly Camera _worldCamera;
        private readonly InputConfiguration _inputConfiguration;
        private readonly Controls _controls;
        private readonly Queue<Vector3> _pathQueue = new();
        
        private Vector3? _currentTarget;
    }
}