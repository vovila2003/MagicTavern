using Modules.Items;

namespace Modules.Inventories
{
    public class StackableItemConfig<T> : ItemConfig<T> where T : StackableItem
    {
        protected override void OnValidate()
        {
            Item.Attributes ??= new object[] {new AttributeStackable()};
        }
    }
}