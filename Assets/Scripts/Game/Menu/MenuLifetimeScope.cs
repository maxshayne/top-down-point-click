using VContainer;
using VContainer.Unity;

namespace Game.Menu
{
    public class MenuLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<MenuPresenter>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<MenuUIView>();
        }
    }
}