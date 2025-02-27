using Tavern.Looting;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class LootingInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<LootInventory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<LootInventoryContext>();
        }
    }
}