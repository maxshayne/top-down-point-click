using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Root.Configuration
{
    [CreateAssetMenu(menuName = "Create ConfigurationData", fileName = "ConfigurationData", order = 0)]
    public class ConfigurationData : ScriptableObject
    {
        [SerializeField] private GameConfiguration gameConfiguration;
        [SerializeField] private LevelConfiguration levelConfiguration;
        [SerializeField] private PlayerConfiguration playerConfiguration;
        [SerializeField] private InputConfiguration inputConfiguration;
        [SerializeField] private AssetReference loadingScreen;
        
        public AssetReference LoadingScreen => loadingScreen;
        public GameConfiguration GameConfiguration => gameConfiguration;
        public LevelConfiguration LevelConfiguration => levelConfiguration;
        public PlayerConfiguration PlayerConfiguration => playerConfiguration;
        public InputConfiguration InputConfiguration => inputConfiguration;
    }
}