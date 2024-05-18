using Game.PlayerInput;
using UnityEngine;
using UnityEngine.AI;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private NavMeshAgent m_NavMeshAgent;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameEntry>();
            builder.Register<PlayerInputService>(Lifetime.Scoped);
            builder.RegisterComponent(m_NavMeshAgent);
        }
    }

}
