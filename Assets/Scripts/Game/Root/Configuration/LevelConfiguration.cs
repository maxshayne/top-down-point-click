using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Root.Configuration
{
    [Serializable]
    public class LevelConfiguration
    {
        public AssetReference LevelAsset => m_LevelAsset;
        public AssetReference PlayerAsset => m_PlayerAsset;
        public string ObstacleTagName => m_ObstacleTagName;

        [SerializeField] private AssetReference m_LevelAsset;
        [SerializeField] private AssetReference m_PlayerAsset;
        [SerializeField] private string m_ObstacleTagName;
    }
}