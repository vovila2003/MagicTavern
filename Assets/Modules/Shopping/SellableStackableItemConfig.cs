using Modules.Inventories;
using Modules.Items;

namespace Modules.Shopping
{
    public abstract class SellableStackableItemConfig<T> : StackableItemConfig<T> where T : Item
    {
        protected override void Awake()
        {
            base.Awake();

            T item = GetItem();
            if (item.Has<ComponentSellable>()) return;

            item.Components?.Add(new ComponentSellable());
            item.SetFlags(ItemFlags.Sellable);
        }
    }
}