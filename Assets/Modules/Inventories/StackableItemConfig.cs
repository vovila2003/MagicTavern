using Modules.Items;

namespace Modules.Inventories
{
    public abstract class StackableItemConfig<T> : ItemConfig<T> where T : Item
    {
        protected override void Awake()
        {
            base.Awake();

            if (Item.Has<ComponentStackable>()) return;

            Item.Components?.Add(new ComponentStackable
            {
                Value = 1
            });
            Item.SetFlags(ItemFlags.Stackable);
        }
    }
}