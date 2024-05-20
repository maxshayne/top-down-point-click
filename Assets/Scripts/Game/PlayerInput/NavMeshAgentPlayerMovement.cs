using Game.Data;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

namespace Game.PlayerInput
{
    [UsedImplicitly]
    public class NavMeshAgentPlayerMovement : IPlayerMovement
    {
        public NavMeshAgentPlayerMovement(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
        }

        public Vector3 CurrentTarget
        {
            get;
            private set;
        }

        public void CreateDestination(Vector3 position)
        {
            _hasTarget = true;
            _navMeshAgent.SetDestination(position);
            CurrentTarget = position;
        }

        public bool IsMoving() => _hasTarget;

        public void ReachDestination()
        {
            _hasTarget = false;
        }

        public void SetTransformValues(SaveData saveData)
        {
            var tr = _navMeshAgent.transform;
            tr.localPosition = saveData.LocalPosition;
            tr.localEulerAngles  = saveData.LocalEulerRotation;
            tr.localScale  = saveData.LocalScale;
            CurrentTarget = saveData.LastPoint;
            _hasTarget = saveData.HasLastPoint;
        }

        public SaveData UpdateState(SaveData state)
        {
            var tr = _navMeshAgent.transform;
            state.LastPoint = CurrentTarget;
            state.LocalPosition = tr.localPosition;
            state.LocalEulerRotation = tr.localEulerAngles;
            state.LocalScale = tr.localScale;
            state.HasLastPoint = _hasTarget;
            return state;
        }

        private readonly NavMeshAgent _navMeshAgent;
        private bool _hasTarget;
    }
}