using Game.UI.Presenters;
using VContainer;
using VContainer.Unity;

namespace Game.Scopes
{
    public class StartGameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<StartScreenPresenter>();
        }
    }
}