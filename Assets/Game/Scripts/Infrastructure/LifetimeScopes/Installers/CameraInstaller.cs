using Tavern.Cameras;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class CameraInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<CameraSetup>();
            builder.RegisterComponentInHierarchy<MinimapCameraSetup>().AsImplementedInterfaces();
            
            builder.Register<GameCursor.GameCursor>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}