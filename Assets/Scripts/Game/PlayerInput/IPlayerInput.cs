using UnityEngine;

namespace Game.PlayerInput
{
    public interface IPlayerInput
    {
        void Subscribe(IInputListener listener);
    }

    public interface IInputListener
    {
        void NotifyPoint(Vector3 position);
    }

    public interface IPlayerMovement : IBuilderAgent<SaveData>
    {
        Vector3? CurrentTarget { get; }
        void CreateDestination(Vector3 position);
        bool IsMoving();
        void ReachDestination();
        void SetTransformValues(SaveData saveData);
    }
}