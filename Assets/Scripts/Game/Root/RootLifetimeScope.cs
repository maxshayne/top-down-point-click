using Game.Data;
using Game.Root.Configuration;
using Infrastructure.AssetManagement;
using Infrastructure.Auth;
using Infrastructure.DataStorage;
using Infrastructure.SceneManagement;
using Infrastructure.Serializers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Root
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private ConfigurationData configurationData;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterConfigData(builder);
            RegisterStorageData(builder);
            RegisterAssetManagement(builder);
        }

        private void RegisterConfigData(IContainerBuilder builder)
        {
            builder.RegisterInstance(configurationData);
            builder.RegisterInstance(configurationData.GameConfiguration);
        }
        
        private void RegisterStorageData(IContainerBuilder builder)
        {
            builder.Register<IDataSerializer, NewtonsoftJsonDataSerializer>(Lifetime.Singleton);
            builder.Register<DataStorageFactory<SaveData>>(Lifetime.Singleton);
            builder.Register(container =>
            {
                var factory = container.Resolve<DataStorageFactory<SaveData>>();
                return factory.Create();
            }, Lifetime.Singleton);
            builder.Register<AuthService>(Lifetime.Singleton);
        }
        
        private void RegisterAssetManagement(IContainerBuilder builder)
        {
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<IAssetProvider, AddressablesAssetProvider>(Lifetime.Singleton);
        }
    }
}