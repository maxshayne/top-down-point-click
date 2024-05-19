using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Root
{
    [CreateAssetMenu(menuName = "Create ConfigurationData", fileName = "ConfigurationData", order = 0)]
    public class ConfigurationData : ScriptableObject
    {
       //private Dictionary<SceneKey, string> _sceneMap
       [SerializeField] private AssetReference m_LoadingScreen;
       
       public AssetReference LoadingScreen => m_LoadingScreen;
    }
}