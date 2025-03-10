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

            builder.Register<MoneySerializer>(Lifetime.Singleton).AsImplementedInterfaces();
            
            var extraSerializers = new Dictionary<string, IExtraSerializer>
            {
                {nameof(ComponentEffect), new ComponentEffectSerializer(_gameSettings.EffectsSettings.EffectsCatalog)},
                {nameof(ComponentDishExtra), new ComponentDishExtraSerializer()}
            };
            builder.Register<ItemSerializer>(Lifetime.Singleton).WithParameter(extraSerializers);

            builder.Register<PlantProductInventorySerializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AnimalProductInventorySerializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<DishInventorySerializer>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}