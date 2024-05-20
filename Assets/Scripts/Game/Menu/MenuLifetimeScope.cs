using VContainer;
using VContainer.Unity;

namespace Game.Menu
{
    public class MenuLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MenuEntry>();
            builder.RegisterComponentInHierarchy<MenuUIView>();
        }
    }
}