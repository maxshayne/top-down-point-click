using System.Collections.Generic;
using System.Linq;
using Game.Data;
using Game.Root.Configuration;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.PlayerInput
{
    [UsedImplicitly]
    public class WaypointsProvider : IPathProvider
    {
        public WaypointsProvider(InputConfiguration inputConfiguration)
        {
            _inputConfiguration = inputConfiguration;
        }

        public void AddPointToPath(Vector3 position)
        {
            if (_pathQueue.Count >= _inputConfiguration.MaxPointsQueue) return;
            _pathQueue.Enqueue(position);
        }

        public bool TryGetNextPoint(out Vector3 position)
        {
            if (_pathQueue.TryDequeue(out position)) return true;
            Debug.LogWarning($"cant get next point, queue count is {_pathQueue.Count}");
            return false;
        }
        
        public SaveData UpdateState(SaveData state)
        {
            state.Points = _pathQueue.ToList();
            return state;
        }

        private readonly InputConfiguration _inputConfiguration;
        private readonly Queue<Vector3> _pathQueue = new();
    }
}