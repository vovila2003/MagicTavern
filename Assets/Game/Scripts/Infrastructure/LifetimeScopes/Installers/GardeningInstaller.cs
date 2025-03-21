using Tavern.Gardening;
using Tavern.Gardening.Fertilizer;
using Tavern.Gardening.Medicine;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class GardeningInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<SeedMaker>(Lifetime.Singleton);

            builder.Register<PotFactory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<PotsController>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            
            builder.RegisterComponentInHierarchy<PotCreatorContext>();

            RegisterMedicine(builder);
            RegisterFertilizer(builder);
        }
        
        private void RegisterMedicine(IContainerBuilder builder)
        {
            builder.Register<MedicineInventory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInHierarchy<MedicineInventoryContext>();
        }

        private void RegisterFertilizer(IContainerBuilder builder)
        {
            builder.Register<FertilizerInventory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInHierarchy<FertilizerInventoryContext>();
            
            builder.RegisterEntryPoint<FertilizerCrafter>().AsSelf();
            builder.RegisterComponentInHierarchy<FertilizerCrafterContext>();
            
            builder.RegisterComponentInHierarchy<FertilizerCookbookContext>();
        }
    }
}