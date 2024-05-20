using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.PlayerInput
{
    [UsedImplicitly]
    public class DefaultPlayerInput : IPlayerInput, IDisposable
    {
        public DefaultPlayerInput(Camera worldCamera)
        {
            _worldCamera = worldCamera;
            _controls = new Controls();
            _controls.Main.Move.started += OnMovePerformed;
            _controls.Enable();
        }
        
        public void Subscribe(IInputListener listener)
        {
            _listeners.Add(listener);
        }
        
        public void Unsubscribe(IInputListener listener)
        {
            _listeners.Remove(listener);
        }
        
        public void Dispose()
        {
            _controls.Disable();
            _controls.Dispose();
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
            if (hit.collider.CompareTag(PlayerTag)) return false;
            return !hit.collider.CompareTag(ObstacleTag);
        }
        
        private readonly Camera _worldCamera;
        private const string PlayerTag = "Player";
        private const string ObstacleTag = "Obstacle";
        private readonly List<IInputListener> _listeners = new();
        private readonly Controls _controls;
    }
}