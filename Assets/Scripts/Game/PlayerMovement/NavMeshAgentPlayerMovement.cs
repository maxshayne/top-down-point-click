using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

namespace Game.PlayerMovement
{
    [UsedImplicitly]
    public class NavMeshAgentPlayerMovement : IPlayerMovement
    {
        public NavMeshAgentPlayerMovement(
            NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
        }

        public void CreateDestination(Vector3 position)
        {
            _hasTarget = true;
            _navMeshAgent.SetDestination(position);
            CreateWaypoint(position);
        }

        public bool IsMoving() => _hasTarget;

        public void ReachDestination() => _hasTarget = false;

        private void CreateWaypoint(Vector3 newPos)
        {
            Debug.Log($"create waypoint in {newPos}");
            var go = new GameObject().AddComponent<WaypointChecker>();
            go.Configure(newPos, PlayerTag);
        }

        private const string PlayerTag = "Player";

        private readonly NavMeshAgent _navMeshAgent;
        private bool _hasTarget;
    }
}