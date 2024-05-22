using Game.PlayerInput;
using Game.PlayerMovement;
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
            builder.RegisterEntryPoint<GameEntry>();
            builder.RegisterComponentInHierarchy<GameUIView>();
            builder.Register<GamePresenter>(Lifetime.Scoped);
            builder.Register<IInputValidator, InputValidator>(Lifetime.Scoped);
            builder.Register<GameLevelConfigurator>(Lifetime.Scoped);
            builder.Register<PlayerLevelConfigurator>(Lifetime.Scoped);
            RegisterGameServices(builder);
            RegisterConfigData(builder);
        }

        private void RegisterGameServices(IContainerBuilder builder)
        {
            builder.RegisterComponent(m_NavMeshAgent);
            builder.RegisterComponent(m_WorldCamera);
            builder.Register<ClickMovementController>(Lifetime.Scoped);
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