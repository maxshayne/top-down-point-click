using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

namespace Game.PlayerMovement
{
    [UsedImplicitly]
    public class NavMeshAgentPlayerMovement : IPlayerMovement
    {
        public NavMeshAgentPlayerMovement(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
            
            _triggerObject= new GameObject().AddComponent<WaypointObject>();
            _triggerObject.Configure(PlayerTag);
        }

        public void CreateDestination(Vector3 position)
        {
            _hasTarget = true;
            _navMeshAgent.SetDestination(position);
            SetTriggerAtDestinationPoint(position);
        }

        public bool IsMoving() => _hasTarget;

        public void ReachDestination() => _hasTarget = false;

        private void SetTriggerAtDestinationPoint(Vector3 newPos)
        {
            _triggerObject.MoveToPoint(newPos);
        }

        private const string PlayerTag = "Player";

        private readonly WaypointObject _triggerObject;
        private readonly NavMeshAgent _navMeshAgent;
        
        private bool _hasTarget;
    }
}