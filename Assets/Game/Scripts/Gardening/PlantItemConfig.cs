using Modules.Inventories;
using Modules.Items;

namespace Tavern.Gardening
{
    public abstract class PlantItemConfig<T> : StackableItemConfig<T> where T : Item
    {
        protected override void Awake()
        {
            base.Awake();
            if (Item.HasComponent<ComponentPlant>()) return;
            
            Item.Components?.Add(new ComponentPlant());
        }
    }
}