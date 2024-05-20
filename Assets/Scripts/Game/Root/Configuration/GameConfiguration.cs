using System;
using UnityEngine;

namespace Game.Root.Configuration
{
    [Serializable]
    public class GameConfiguration
    {
        public DataStorageType GetSavingSystem()
        {
            return m_SavingSystem;
        }
        
        [SerializeField]
        private DataStorageType m_SavingSystem;
    }

    public enum DataStorageType
    {
        Local,
        Cloud
    }
}