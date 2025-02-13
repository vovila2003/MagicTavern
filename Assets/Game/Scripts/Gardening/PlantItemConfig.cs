using Modules.Items;
using Modules.Shopping;

namespace Tavern.Gardening
{
    public abstract class PlantItemConfig<T> : SellableStackableItemConfig<T> where T : Item
    {
        protected override void Awake()
        {
            base.Awake();
            T item = GetItem();
            if (item.Has<ComponentPlant>()) return;
            
            item.Components?.Add(new ComponentPlant());
        }
    }
}