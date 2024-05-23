using System.Collections.Generic;
using System.Linq;
using Game.Root.Configuration;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.PlayerMovement
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

        public bool TryPeekNextPoint(out Vector3 position)
        {
            if (_pathQueue.TryPeek(out position)) return true;
            Debug.LogWarning($"cant get next point, queue count is {_pathQueue.Count}");
            return false;
        }
        
        public void RemovePoint() => _pathQueue.Dequeue();
        
        public List<Vector3> GetPath() => _pathQueue.ToList();

        private readonly InputConfiguration _inputConfiguration;
        private readonly Queue<Vector3> _pathQueue = new();
    }
}