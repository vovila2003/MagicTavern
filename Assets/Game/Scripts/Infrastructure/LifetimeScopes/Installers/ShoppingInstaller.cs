using Tavern.Shopping;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class ShoppingInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<CharacterBuyer>(Lifetime.Singleton);
            builder.Register<CharacterSeller>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            
            builder.Register<ShopFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}