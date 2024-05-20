using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Root.Configuration
{
    [CreateAssetMenu(menuName = "Create ConfigurationData", fileName = "ConfigurationData", order = 0)]
    public class ConfigurationData : ScriptableObject
    {
        public AssetReference LoadingScreen => m_LoadingScreen;
        public GameConfiguration GameConfiguration => m_GameConfiguration;
        public LevelConfiguration LevelConfiguration => m_LevelConfiguration;
        public PlayerConfiguration PlayerConfiguration => m_PlayerConfiguration;

        [SerializeField] private GameConfiguration m_GameConfiguration;
        [SerializeField] private LevelConfiguration m_LevelConfiguration;
        [SerializeField] private PlayerConfiguration m_PlayerConfiguration;
        [SerializeField] private AssetReference m_LoadingScreen;
    }
}