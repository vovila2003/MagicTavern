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
        
        public SaveLoadInstaller(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public void Install(IContainerBuilder builder)
        {
            RegisterSaveLoadSystemCore(builder);
            RegisterResourceStoragesSerializers(builder);
            RegisterItemSerializer(builder);
            RegisterInventorySerializers(builder);
            RegisterCookbookSerializers(builder);
            RegisterCharacterSerializers(builder);
            RegisterKitchenItemSerializers(builder);
            RegisterShopSerializers(builder);
            RegisterPotSerializers(builder);
            RegisterTimeGameCycleSerializers(builder);
        }

        private void RegisterSaveLoadSystemCore(IContainerBuilder builder)
        {
            builder.Register<AesEncryptor>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .WithParameter(new AesEncryptor.AesData
                {
                    Key = _gameSettings.SaveLoadSettings.Key,
                    Iv = _gameSettings.SaveLoadSettings.InitializationVector
                });
            
            builder.Register<GameRepository>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .WithParameter(new GameRepository.Params
                {
                    FileName = Path.Combine(Application.persistentDataPath, 
                                            _gameSettings.SaveLoadSettings.FileSaveName),
                    UseCompression = _gameSettings.SaveLoadSettings.UseCompression,
                    UseEncryption = _gameSettings.SaveLoadSettings.UseEncryption
                });
            
            builder.Register<GameSaveLoader>(Lifetime.Singleton);
        }

        private void RegisterItemSerializer(IContainerBuilder builder)
        {
            builder.Register<ItemSerializer>(Lifetime.Singleton)
                .WithParameter(new Dictionary<string, IExtraSerializer>
                {
                    {nameof(ComponentEffect), new ComponentEffectSerializer(_gameSettings.EffectsSettings.EffectsCatalog)},
                    {nameof(ComponentDishExtra), new ComponentDishExtraSerializer()}
                });
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

        private void RegisterKitchenItemSerializers(IContainerBuilder builder)
        {
            builder.Register<KitchenItemsSerializer>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterShopSerializers(IContainerBuilder builder)
        {
            builder.Register<ShopsSerializer>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterPotSerializers(IContainerBuilder builder)
        {
            builder.Register<PotsSerializer>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterTimeGameCycleSerializers(IContainerBuilder builder)
        {
            builder.Register<TimeGameCycleSerializer>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}