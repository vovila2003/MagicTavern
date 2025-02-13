using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class ShoppingInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            // builder.Register<GoodsBuyCondition_CanSpendMoney>(Lifetime.Singleton).AsImplementedInterfaces();
            //
            // builder.Register<GoodsBuyProcessor_SpendMoney>(Lifetime.Singleton).AsImplementedInterfaces();
            //
            // builder.Register<DishItemBuyCompleter>(Lifetime.Singleton).AsImplementedInterfaces();
            // builder.Register<FertilizerItemBuyCompleter>(Lifetime.Singleton).AsImplementedInterfaces();
            // builder.Register<KitchenItemBuyCompleter>(Lifetime.Singleton).AsImplementedInterfaces();
            // builder.Register<LootItemBuyCompleter>(Lifetime.Singleton).AsImplementedInterfaces();
            // builder.Register<MedicineItemBuyCompleter>(Lifetime.Singleton).AsImplementedInterfaces();
            // builder.Register<ProductBuyCompleter>(Lifetime.Singleton).AsImplementedInterfaces();
            // builder.Register<SeedBuyCompleter>(Lifetime.Singleton).AsImplementedInterfaces();
            //
            // builder.Register<WaterBuyCompleter>(Lifetime.Singleton).AsImplementedInterfaces();
            //
            // builder.Register<GoodsBuyer>(Lifetime.Singleton).AsSelf();
            //
            // builder.RegisterComponentInHierarchy<Shop>();
        }
    }
}