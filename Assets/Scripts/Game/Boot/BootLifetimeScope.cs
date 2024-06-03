using VContainer;
using VContainer.Unity;

namespace Game.Boot
{
    public class BootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {            
            builder.RegisterEntryPoint<BootEntry>();            
        }
    }
}