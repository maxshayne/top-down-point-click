using System;
using UnityEngine;

namespace Game.Root.Configuration
{
    [Serializable]
    public class GameConfiguration
    {
        [SerializeField] private DataStorageType savingSystem;
        
        public DataStorageType GetDataStorageType() => savingSystem;
    }

    public enum DataStorageType
    {
        Local,
        Cloud
    }
}