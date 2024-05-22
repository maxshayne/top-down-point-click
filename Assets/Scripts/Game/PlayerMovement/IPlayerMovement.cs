using Game.Data;
using Infrastructure.DataStorage;
using UnityEngine;

namespace Game.PlayerMovement
{
    public interface IPlayerMovement : IBuilderAgent<SaveData>
    {
        Vector3 CurrentTarget { get; }
        void CreateDestination(Vector3 position);
        bool IsMoving();
        void ReachDestination();
        void LoadState(SaveData data);
    }
}