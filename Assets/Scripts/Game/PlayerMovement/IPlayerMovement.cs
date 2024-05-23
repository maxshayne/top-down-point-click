using UnityEngine;

namespace Game.PlayerMovement
{
    public interface IPlayerMovement
    {
        void CreateDestination(Vector3 position);
        bool IsMoving();
        void ReachDestination();
    }
}