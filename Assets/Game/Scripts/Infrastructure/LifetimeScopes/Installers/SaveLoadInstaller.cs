using System.Collections.Generic;
using System.IO;
using Modules.SaveLoad;
using Tavern.Cooking;
using Tavern.Effects;
using Tavern.Settings;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class SaveLoadInstaller : IInstaller
    {
        private readonly GameSettings _gameSettings;
        private readonly string _filePath;
        
        public SaveLoadInstaller(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _filePath = Path.Combine(Application.persistentDataPath, gameSettings.SaveLoadSettings.FileSaveName);
        }

        public void Install(IContainerBuilder builder)
        {
            builder.Register<GameRepository>(Lifetime.Singleton).AsImplementedInterfaces().WithParameter(_filePath);
            builder.Register<GameSaveLoader>(Lifetime.Singleton);

            RegisterResourceStoragesSerializers(builder);

            var extraSerializers = new Dictionary<string, IExtraSerializer>
            {
                {nameof(ComponentEffect), new ComponentEffectSerializer(_gameSettings.EffectsSettings.EffectsCatalog)},
                {nameof(ComponentDishExtra), new ComponentDishExtraSerializer()}
            };
            builder.Register<ItemSerializer>(Lifetime.Singleton).WithParameter(extraSerializers);

            RegisterInventorySerializers(builder);
            RegisterCookbookSerializers(builder);
            RegisterCharacterSerializers(builder);
        }

        private static void RegisterResourceStoragesSerializers(IContainerBuilder builder)
        {
            builder.Register<MoneySerializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<WaterSerializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SlopsSerializer>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private static void RegisterInventorySerializers(IContainerBuilder builder)
        {
            builder.Register<PlantProductInventorySerializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AnimalProductInventorySerializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<DishInventorySerializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FertilizerInventorySerializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MedicineInventorySerializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SeedInventorySerializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<LootInventorySerializer>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private static void RegisterCookbookSerializers(IContainerBuilder builder)
        {
            builder.Register<DishCookbookSerializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<DishAutoCookbookSerializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FertilizerCookbookSerializer>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterCharacterSerializers(IContainerBuilder builder)
        {
            builder.Register<CharacterSerializer>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}