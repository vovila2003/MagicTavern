using Tavern.InputServices;
using Tavern.Settings;
using Tavern.UI;
using Tavern.UI.Presenters;
using Tavern.UI.Views;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class UiInstaller : IInstaller
    {
        private readonly UISceneSettings _sceneSettings;
        private readonly GameSettings _settings;

        public UiInstaller(UISceneSettings sceneSettings, GameSettings settings)
        {
            _sceneSettings = sceneSettings;
            _settings = settings;
        }
        
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_sceneSettings);
            builder.RegisterInstance(_settings.UISettings);

            builder.RegisterComponentInHierarchy<UiManager>().AsImplementedInterfaces().AsSelf();
            builder.Register<MouseClickInputService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<ViewsFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CommonPresentersFactory>(Lifetime.Singleton);
            builder.Register<CookingPresentersFactory>(Lifetime.Singleton);
        }
    }
}