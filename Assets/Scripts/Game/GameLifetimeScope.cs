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
            builder.RegisterComponentInHierarchy<GameUIView>();
            builder.RegisterEntryPoint<GameEntry>();
            builder.RegisterEntryPoint<GameLevelConfigurator>();
            builder.Register<PlayerLevelConfigurator>(Lifetime.Scoped);
            RegisterGameServices(builder);
            RegisterConfigData(builder);
        }

        private void RegisterGameServices(IContainerBuilder builder)
        {
            builder.RegisterComponent(m_NavMeshAgent);
            builder.RegisterComponent(m_WorldCamera);
            builder.Register<PlayerInputService>(Lifetime.Scoped);
            builder.Register<IPlayerInput, DefaultPlayerInput>(Lifetime.Scoped);
            builder.Register<IPlayerMovement, NavMeshAgentPlayerMovement>(Lifetime.Scoped);
        }

        private void RegisterConfigData(IContainerBuilder builder)
        {
            builder.Register(
                resolver => resolver.Resolve<ConfigurationData>().LevelConfiguration,
                Lifetime.Scoped); 
            builder.Register(
                resolver => resolver.Resolve<ConfigurationData>().PlayerConfiguration,
                Lifetime.Scoped); 
            builder.Register(
                resolver => resolver.Resolve<ConfigurationData>().InputConfiguration,
                Lifetime.Scoped);
        }
    }
}