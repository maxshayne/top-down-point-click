using Game.PlayerInput;
using Infrastructure.DataStorage;
using Infrastructure.DataStorage.Implementations;
using Infrastructure.Serializers.Implementations;
using UnityEngine;
using UnityEngine.AI;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private NavMeshAgent m_NavMeshAgent;
        [SerializeField] private Camera m_WorldCamera;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PlayerInputService>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<UIView>();
            builder.RegisterComponent(m_NavMeshAgent);
            builder.RegisterComponent(m_WorldCamera);
            builder.RegisterEntryPoint<GameEntry>();
            
            builder.Register<IDataSerializer, NewtonsoftJsonDataSerializer>(Lifetime.Singleton);
            builder.Register<IDataStorage<SaveData>, CloudSaveDataStorage<SaveData>>(Lifetime.Singleton);
            builder.Register<AuthService>(Lifetime.Singleton);
        }
    }
}