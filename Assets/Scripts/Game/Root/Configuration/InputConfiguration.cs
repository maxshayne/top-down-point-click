using System;
using UnityEngine;

namespace Game.Root.Configuration
{
    [Serializable]
    public class InputConfiguration
    {
        public int MaxPointsQueue => m_MaxPointsQueue;
        
        [SerializeField] private int m_MaxPointsQueue;
    }
}