using Tavern.Gardening;
using Tavern.ProductsAndIngredients;
using Tavern.Storages;
using Tavern.Storages.CurrencyStorages;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class StoragesInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<PlantProductInventory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInHierarchy<PlantProductInventoryContext>();
            
            builder.Register<AnimalProductInventory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInHierarchy<AnimalProductInventoryContext>();
            
            builder.Register<SeedInventory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInHierarchy<SeedInventoryContext>();
            
            builder.RegisterComponentInHierarchy<WaterStorage>().AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInHierarchy<SlopsStorage>().AsImplementedInterfaces().AsSelf();
            
            builder.RegisterComponentInHierarchy<MoneyStorage>().AsImplementedInterfaces().AsSelf();
            
            builder.Register<InventoryFacade>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<InventoryFacadeContext>();
        }
    }
}