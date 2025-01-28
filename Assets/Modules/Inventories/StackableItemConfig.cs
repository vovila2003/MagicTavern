using Modules.Items;

namespace Modules.Inventories
{
    public abstract class StackableItemConfig<T> : ItemConfig<T> where T : Item
    {
        protected override void Awake()
        {
            base.Awake();

            T item = GetItem();
            if (item.Has<ComponentStackable>()) return;

            item.Components?.Add(new ComponentStackable
            {
                Value = 1
            });
            item.SetFlags(ItemFlags.Stackable);
        }
    }
}