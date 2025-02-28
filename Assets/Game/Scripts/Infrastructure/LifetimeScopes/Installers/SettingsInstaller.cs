using Tavern.Settings;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class SettingsInstaller : IInstaller
    {
        private readonly GameSettings _gameSettings;
        private readonly SceneSettings _sceneSettings;

        public SettingsInstaller(
            GameSettings gameSettings, 
            SceneSettings sceneSettings)
        {
            _gameSettings = gameSettings;
            _sceneSettings = sceneSettings;
        }

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameSettings);
            builder.RegisterInstance(_sceneSettings);
        }
    }
}