using Modules.Items;

namespace Modules.Shopping
{
    public abstract class SellableItemConfig : ItemConfig
    {
        protected override void Awake()
        {
            base.Awake();

            if (Has<ComponentSellable>()) return;

            Components?.Add(new ComponentSellable());
            SetFlags(ItemFlags.Sellable);
        }
    }
}