using Modules.Items;

namespace Modules.Inventories
{
    public abstract class StackableItemConfig : ItemConfig
    {
        protected override void Awake()
        {
            base.Awake();

            if (Has<ComponentStackable>()) return;

            Components?.Add(new ComponentStackable
            {
                Value = 1
            });
            SetFlags(ItemFlags.Stackable);
        }
    }
}