using Tavern.InputServices;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class InputInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<InputService>();
        }
    }
}