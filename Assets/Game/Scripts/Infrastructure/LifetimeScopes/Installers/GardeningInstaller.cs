using Tavern.Gardening;
using Tavern.Gardening.Fertilizer;
using Tavern.Gardening.Medicine;
using Tavern.Settings;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class GardeningInstaller : IInstaller
    {
        private readonly GameSettings _gameSettings;
        private readonly SceneSettings _sceneSettings;

        public GardeningInstaller(GameSettings gameSettings, SceneSettings sceneSettings)
        {
            _gameSettings = gameSettings;
            _sceneSettings = sceneSettings;
        }

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameSettings.SeedMakerSettings);
            builder.RegisterInstance(_gameSettings.PotPrefab);
            builder.RegisterComponentInHierarchy<SeedMaker>();

            builder.Register<PotsController>(Lifetime.Singleton)
                .AsImplementedInterfaces().AsSelf().WithParameter(_sceneSettings.WorldTransform);
            builder.RegisterComponentInHierarchy<PotCreator>();

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