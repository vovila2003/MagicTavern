using Tavern.Cameras;
using Tavern.Minimap;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class MinimapInstaller : IInstaller
    {
        
        public void Install(IContainerBuilder builder)
        {
            // minimap 1
            builder.RegisterComponentInHierarchy<MinimapCameraSetup>().AsImplementedInterfaces();
            
            // minimap 2
            builder.Register<MinimapService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<MiniMapContext>();
        }
    }
}