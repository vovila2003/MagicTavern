using Modules.Items;

namespace Modules.Inventories
{
    public class StackableItemConfig<T> : ItemConfig<T> where T : Item
    {
        protected override void Awake()
        {
            base.Awake();
            if (Item.HasAttribute<AttributeStackable>()) return;
            
            Item.Attributes?.Add(new AttributeStackable());
            Item.SetFlags(ItemFlags.Stackable);
        }
    }
}