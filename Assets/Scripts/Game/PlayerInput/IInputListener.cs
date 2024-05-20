using UnityEngine;

namespace Game.PlayerInput
{
    public interface IInputListener
    {
        void NotifyPoint(Vector3 position);
    }
}