using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.PlayerInput
{
    public class DefaultPlayerInput : IPlayerInput
    {
        public DefaultPlayerInput(Camera worldCamera)
        {
            _worldCamera = worldCamera;
            var controls = new Controls();
            controls.Main.Move.started += OnMovePerformed;
            controls.Enable();
        }
        
        public void Subscribe(IInputListener listener)
        {
            _listeners.Add(listener);
        }
        
        private void OnMovePerformed(InputAction.CallbackContext obj)
        {
            if (!TryGetHitPosition(out var newPosition)) return;
            Notify(newPosition);
        }

        private void Notify(Vector3 position)
        {
            foreach (var inputListener in _listeners)
            {
                inputListener.NotifyPoint(position);
            }
        }

        private bool TryGetHitPosition(out Vector3 pos)
        {
            pos = Vector3.zero;
            var ray = _worldCamera.ScreenPointToRay(Pointer.current.position.value);
            if (!Physics.Raycast(ray, out var hit)) return false;
            pos = hit.point;
            if (hit.collider.CompareTag("Player")) return false;
            return !hit.collider.CompareTag(ObstacleTag);
        }
        
        private readonly Camera _worldCamera;
        private const string ObstacleTag = "Obstacle";
        private readonly List<IInputListener> _listeners = new();
    }
}