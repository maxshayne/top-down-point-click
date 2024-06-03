using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Root.Configuration
{
    [Serializable]
    public class LevelConfiguration
    {
        [SerializeField] private AssetReference levelAsset;
        
        public AssetReference LevelAsset => levelAsset;
    }
}