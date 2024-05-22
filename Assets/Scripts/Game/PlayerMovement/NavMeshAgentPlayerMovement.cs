using Game.Data;
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

        public Vector3 CurrentTarget
        {
            get;
            private set;
        }
        
        public void LoadState(SaveData data)
        {
            var tr = _navMeshAgent.transform;
            tr.localPosition = data.LocalPosition;
            tr.localEulerAngles  = data.LocalEulerRotation;
            tr.localScale  = data.LocalScale;
            CurrentTarget = data.LastPoint;
            _hasTarget = data.HasLastPoint;
        }

        public void CreateDestination(Vector3 position)
        {
            _hasTarget = true;
            _navMeshAgent.SetDestination(position);
            CurrentTarget = position;
            CreateWaypoint(position);
        }

        public bool IsMoving() => _hasTarget;

        public void ReachDestination()
        {
            _hasTarget = false;
        }

        public SaveData UpdateState(SaveData state)
        {
            state.LastPoint = CurrentTarget;
            state.HasLastPoint = _hasTarget;
            return state;
        }
        
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