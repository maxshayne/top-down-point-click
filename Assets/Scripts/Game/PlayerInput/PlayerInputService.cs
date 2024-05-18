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
        private Controls _controls;
        private readonly Queue<Vector3> _pointsQueue = new();
        private Vector3? _nextTarget;
        private NavMeshAgent _agent;
        private readonly NavMeshAgent _player;
        
        public PlayerInputService(NavMeshAgent player)
        {
            _player = player;
            Configure();
        }
        
        private void Configure()
        {
            _agent = _player.GetComponent<NavMeshAgent>();
            _controls = new Controls();
            _controls.Main.Move.performed += OnMovePerformed;
            _controls.Enable();
        }

        private void OnMovePerformed(InputAction.CallbackContext obj)
        {
            Debug.Log($"click");
            AddNewPoint();
        }
        
        private void AddNewPoint()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;
            var newPos = hit.point;
            var go = new GameObject().AddComponent<WaypointChecker>();
            go.Configure(newPos, Callback, _agent.tag);
            _pointsQueue.Enqueue(newPos); 
            Debug.Log(_agent.velocity.magnitude);
            if (_agent.velocity.magnitude > 0) return;
            Move();
        }
        
        private void Move()
        {
            Debug.Log("move");
            if (_pointsQueue.TryDequeue(out var pos))
            {
                _agent.SetDestination(pos);
            }
        }

        private void Callback()
        {
            Move();
        }
    }
}