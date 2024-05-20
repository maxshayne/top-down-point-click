using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Root.Configuration
{
    [CreateAssetMenu(menuName = "Create ConfigurationData", fileName = "ConfigurationData", order = 0)]
    public class ConfigurationData : ScriptableObject
    {
        [SerializeField] private GameConfiguration m_GameConfiguration;
        [SerializeField] private AssetReference m_LoadingScreen;

        public AssetReference LoadingScreen => m_LoadingScreen;
        public GameConfiguration GameConfiguration => m_GameConfiguration;
    }
}