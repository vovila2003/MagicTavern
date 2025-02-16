using Tavern.Shopping.Buying;
using Tavern.Shopping.Shop;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class ShoppingInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<Buyer>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<Shop>();
        }
    }
}