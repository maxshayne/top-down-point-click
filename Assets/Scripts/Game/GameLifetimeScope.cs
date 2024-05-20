using Game.PlayerInput;
using Game.Root.Configuration;
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
            builder.RegisterComponentInHierarchy<GameUIView>();
            builder.RegisterComponent(m_NavMeshAgent);
            builder.RegisterComponent(m_WorldCamera);
            builder.RegisterEntryPoint<GameEntry>();
            builder.RegisterEntryPoint<GameLevelConfigurator>();
            builder.Register<PlayerLevelConfigurator>(Lifetime.Scoped);
            builder.Register(
                resolver => resolver.Resolve<ConfigurationData>().LevelConfiguration,
                Lifetime.Scoped); 
            builder.Register(
                resolver => resolver.Resolve<ConfigurationData>().PlayerConfiguration,
                Lifetime.Scoped); 
        }
    }
}