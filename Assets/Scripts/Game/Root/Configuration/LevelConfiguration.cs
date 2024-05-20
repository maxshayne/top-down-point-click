using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Root.Configuration
{
    [Serializable]
    public class LevelConfiguration
    {
        public AssetReference LevelAsset => m_LevelAsset;
        
        [SerializeField] private AssetReference m_LevelAsset;
    }
}