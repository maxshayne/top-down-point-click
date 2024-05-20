using System;
using UnityEngine;

namespace Game.Root.Configuration
{
    [Serializable]
    public class PlayerConfiguration
    {
        public float MoveSpeed => m_MoveSpeed;
        public float RotationSpeed => m_RotationSpeed;
        
        [SerializeField] private float m_MoveSpeed;
        [SerializeField] private float m_RotationSpeed;
    }
}