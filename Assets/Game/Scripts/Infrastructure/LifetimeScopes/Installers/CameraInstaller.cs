using Tavern.Cameras;
using Tavern.Settings;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class CameraInstaller : IInstaller
    {
        private readonly GameSettings _gameSettings;

        public CameraInstaller(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameSettings.CameraSettings);
            builder.RegisterComponentInHierarchy<CameraSetup>();
            
            RegisterCursor(builder);
        }

        private void RegisterCursor(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameSettings.CursorSettings);
            builder.Register<GameCursor.GameCursor>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}