using Tavern.Cooking;
using Tavern.Cooking.MiniGame;
using Tavern.InputServices;
using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class CookingInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<KitchenInventory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<KitchenInventoryContext>();

            builder.Register<ActiveDishRecipe>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<DishCrafter>(Lifetime.Singleton);
            
            builder.Register<DishInventory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<DishInventoryContext>();

            builder.RegisterComponentInHierarchy<DishCookbookContext>();
            builder.RegisterComponentInHierarchy<DishAutoCookbookContext>();
            
            builder.Register<RecipeMatcher>(Lifetime.Singleton);

            builder.RegisterEntryPoint<MiniGameInputService>();
            builder.RegisterEntryPoint<MiniGame>().AsSelf();
            builder.Register<MiniGamePlayer>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            builder.Register<KitchenItemFactory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}