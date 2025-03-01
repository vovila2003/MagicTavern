using Tavern.TestAndDebug;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class DebugInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<LightController>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<WeekView>().AsImplementedInterfaces();
        }
    }
}