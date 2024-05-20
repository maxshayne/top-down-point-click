using Game.Root.Configuration;
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
            builder.RegisterInstance(m_ConfigurationData);
            builder.RegisterInstance(m_ConfigurationData.GameConfiguration);
            builder.Register<IDataSerializer, NewtonsoftJsonDataSerializer>(Lifetime.Singleton);
            builder.Register<DataStorageFactory>(Lifetime.Singleton);
            builder.Register(container =>
            {
                var factory = container.Resolve<DataStorageFactory>();
                return factory.Create();
            }, Lifetime.Singleton);
            builder.Register<DataBuilder<SaveData>>(Lifetime.Singleton);
            builder.Register<AuthService>(Lifetime.Singleton);
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.RegisterEntryPoint<BootEntry>();
        }
    }
}