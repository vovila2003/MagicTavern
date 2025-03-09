using System.IO;
using Modules.SaveLoad;
using Tavern.Settings;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class SaveLoadInstaller : IInstaller
    {
        private readonly string _filePath;
        
        public SaveLoadInstaller(GameSettings gameSettings)
        {
            _filePath = Path.Combine(Application.persistentDataPath, gameSettings.SaveLoadSettings.FileSaveName);
        }

        public void Install(IContainerBuilder builder)
        {
            builder.Register<GameRepository>(Lifetime.Singleton).AsImplementedInterfaces().WithParameter(_filePath);
            builder.Register<GameSaveLoader>(Lifetime.Singleton);
            
            builder.Register<MoneySerializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlantProductSerializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AnimalProductSerializer>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}