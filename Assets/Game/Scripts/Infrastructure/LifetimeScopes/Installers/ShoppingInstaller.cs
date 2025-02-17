using Tavern.Settings;
using Tavern.Shopping;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class ShoppingInstaller : IInstaller
    {
        private readonly GameSettings _gameSettings;

        public ShoppingInstaller(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameSettings.ShoppingSettings);
            
            builder.Register<Buyer>(Lifetime.Singleton);
            builder.Register<CharacterSeller>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            
            builder.Register<ShopFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}