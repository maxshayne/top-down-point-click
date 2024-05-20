using Game.PlayerInput;
using Game.Root.Configuration;
using UnityEngine;
using UnityEngine.AI;
using VContainer;
using VContainer.Unity;

namespace Game.Gameplay
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<GameUIView>();
            builder.RegisterEntryPoint<GameEntry>();
            builder.Register<GameLevelConfigurator>(Lifetime.Scoped);
            builder.Register<PlayerLevelConfigurator>(Lifetime.Scoped);
            RegisterGameServices(builder);
            RegisterConfigData(builder);
        }

        private void RegisterGameServices(IContainerBuilder builder)
        {
            builder.RegisterComponent(m_NavMeshAgent);
            builder.RegisterComponent(m_WorldCamera);
            builder.Register<PlayerMovementController>(Lifetime.Scoped);
            builder.Register<IPlayerInput, DefaultPlayerInput>(Lifetime.Scoped);
            builder.Register<IPathProvider, WaypointsProvider>(Lifetime.Scoped);
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
        
        [SerializeField] private NavMeshAgent m_NavMeshAgent;
        [SerializeField] private Camera m_WorldCamera;
    }
}