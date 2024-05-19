using Infrastructure.DataStorage;
using Infrastructure.DataStorage.Implementations;
using Infrastructure.Serializers.Implementations;
using VContainer;
using VContainer.Unity;

namespace Game.Root
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IDataSerializer, NewtonsoftJsonDataSerializer>(Lifetime.Singleton);
            builder.Register<IDataStorage<SaveData>, CloudSaveDataStorage<SaveData>>(Lifetime.Singleton);
            builder.Register<AuthService>(Lifetime.Singleton);
        }
        
    }
}