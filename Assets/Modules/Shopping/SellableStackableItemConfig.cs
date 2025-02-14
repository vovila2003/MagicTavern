using Modules.Inventories;
using Modules.Items;

namespace Modules.Shopping
{
    public abstract class SellableStackableItemConfig : StackableItemConfig
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