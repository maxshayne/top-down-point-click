using UnityEngine;

namespace Game.PlayerInput
{
    public interface IInputValidator
    {
        bool TryValidateClick(Vector3 clickPosition, out Vector3 pos);
    }
}