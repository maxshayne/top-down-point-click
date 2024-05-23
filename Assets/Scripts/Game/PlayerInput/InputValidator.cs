using JetBrains.Annotations;
using UnityEngine;

namespace Game.PlayerInput
{
    [UsedImplicitly]
    public class InputValidator : IInputValidator
    {
        public InputValidator(Camera worldCamera)
        {
            _worldCamera = worldCamera;
        }
        
        public bool TryValidateClick(Vector3 clickPosition, out Vector3 pos)
        {
            pos = Vector3.zero;
            var ray = _worldCamera.ScreenPointToRay(clickPosition);
            if (!Physics.Raycast(ray, out var hit)) return false;
            pos = hit.point;
            if (hit.collider.CompareTag(PlayerTag)) return false;
            return !hit.collider.CompareTag(ObstacleTag);
        }
        
        private const string PlayerTag = "Player";
        private const string ObstacleTag = "Obstacle";
        
        private readonly Camera _worldCamera;
    }

}