using System;
using UnityEngine;

namespace Game.Root.Configuration
{
    [Serializable]
    public class InputConfiguration
    {
        [SerializeField] private int maxPointsQueue;
        
        public int MaxPointsQueue => maxPointsQueue;
    }
}