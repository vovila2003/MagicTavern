using Modules.Items;

namespace Modules.Inventories
{
    public abstract class StackableItemConfig<T> : ItemConfig<T> where T : Item
    {
        protected override void Awake()
        {
            base.Awake();
            if (Item.HasComponent<ComponentStackable>()) return;
            
            Item.Components?.Add(new ComponentStackable());
            Item.SetFlags(ItemFlags.Stackable);
        }
    }
}