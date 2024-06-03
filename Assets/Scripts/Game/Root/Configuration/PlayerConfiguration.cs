using System;
using UnityEngine;

namespace Game.Root.Configuration
{
    [Serializable]
    public class PlayerConfiguration
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        
        public float MoveSpeed => moveSpeed;
        public float RotationSpeed => rotationSpeed;
    }
}