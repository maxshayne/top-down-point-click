using UnityEngine;
using UnityEngine.AI;

namespace Game.PlayerInput
{
    public class NavMeshAgentPlayerMovement : IPlayerMovement
    {
        public NavMeshAgentPlayerMovement(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
        }

        public Vector3? CurrentTarget => _currentTarget;

        public void CreateDestination(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
            _currentTarget = position;
        }

        public bool IsMoving() => CurrentTarget.HasValue;

        public void ReachDestination()
        {
            _currentTarget = null;
        }

        public void SetTransformValues(SaveData saveData)
        {
            var tr = _navMeshAgent.transform;
            tr.localPosition = saveData.LocalPosition;
            tr.localEulerAngles  = saveData.LocalEulerRotation;
            tr.localScale  = saveData.LocalScale;
            _currentTarget = saveData.LastPoint;
        }
        
        public SaveData SavePlayerState()
        {
            var tr = _navMeshAgent.transform;
            var state = new SaveData
            {
                LastPoint = _currentTarget,
                LocalPosition = tr.localPosition,
                LocalEulerRotation = tr.localEulerAngles,
                LocalScale = tr.localScale,
                //Points = _pathQueue.ToList()
            };
            return state;
        }

        private readonly NavMeshAgent _navMeshAgent;
        private Vector3? _currentTarget;
        
        public SaveData UpdateState(SaveData state)
        {
            var tr = _navMeshAgent.transform;
            state.LastPoint = _currentTarget;
            state.LocalPosition = tr.localPosition;
            state.LocalEulerRotation = tr.localEulerAngles;
            state. LocalScale = tr.localScale;
            return state;
        }
    }
}