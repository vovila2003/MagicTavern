using Tavern.Minimap;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class MinimapInstaller : IInstaller
    {
        
        public void Install(IContainerBuilder builder)
        {
            builder.Register<MinimapService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}