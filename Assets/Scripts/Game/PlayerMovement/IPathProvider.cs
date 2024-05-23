using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerMovement
{
    public interface IPathProvider 
    {
        void AddPointToPath(Vector3 position);
        bool TryPeekNextPoint(out Vector3 position);
        void RemovePoint();
        List<Vector3> GetPath();
    }
}