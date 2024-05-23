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
        public DefaultPlayerInput()
        {
            _controls = new Controls();
            _controls.Main.Move.started += OnInputReceived;
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
        
        private void OnInputReceived(InputAction.CallbackContext obj)
        {
            Notify(Pointer.current.position.value);
        }

        private void Notify(Vector3 clickPosition)
        {
            foreach (var inputListener in _listeners)
            {
                inputListener.NotifyInput(clickPosition);
            }
        }
        
        private readonly List<IInputListener> _listeners = new();
        private readonly Controls _controls;
    }
}