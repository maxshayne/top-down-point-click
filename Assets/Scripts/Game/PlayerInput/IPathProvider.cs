using Game.Data;
using Infrastructure.DataStorage;
using UnityEngine;

namespace Game.PlayerInput
{
    public interface IPathProvider : IBuilderAgent<SaveData>
    {
        void AddPointToPath(Vector3 position);
        bool TryGetNextPoint(out Vector3 position);
    }
}