using UnityEngine;

namespace Game.PlayerInput
{
    public interface IInputListener
    {
        void NotifyInput(Vector3 position);
    }
}