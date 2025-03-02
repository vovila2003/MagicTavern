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
            builder.RegisterComponentInHierarchy<SeedMaker>();

            builder.Register<PotFactory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<PotsController>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.RegisterComponentInHierarchy<PotCreatorContext>();

            RegisterMedicine(builder);
            RegisterFertilizer(builder);
        }
        
        private void RegisterMedicine(IContainerBuilder builder)
        {
            builder.Register<MedicineConsumer>(Lifetime.Singleton).AsSelf();
            builder.Register<MedicineInventory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<MedicineInventoryContext>();
        }

        private void RegisterFertilizer(IContainerBuilder builder)
        {
            builder.Register<FertilizerConsumer>(Lifetime.Singleton).AsSelf();
            builder.Register<FertilizerInventory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<FertilizerInventoryContext>();
            
            builder.RegisterEntryPoint<FertilizerCrafter>().AsSelf();
            builder.RegisterComponentInHierarchy<FertilizerCrafterContext>();
        }
    }
}