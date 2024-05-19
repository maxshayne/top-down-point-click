using Game.Root.SceneManagement;
using Infrastructure.DataStorage;
using Infrastructure.DataStorage.Implementations;
using Infrastructure.Serializers.Implementations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Root
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private ConfigurationData m_ConfigurationData;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IDataSerializer, NewtonsoftJsonDataSerializer>(Lifetime.Singleton);
            builder.Register<IDataStorage<SaveData>, CloudSaveDataStorage<SaveData>>(Lifetime.Singleton);
            builder.Register<AuthService>(Lifetime.Singleton);
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.RegisterInstance(m_ConfigurationData);
            builder.RegisterEntryPoint<BootEntry>();
        }
    }
}