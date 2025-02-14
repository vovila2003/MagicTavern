using Modules.Items;
using Modules.Shopping;

namespace Tavern.Gardening
{
    public abstract class PlantItemConfig : SellableStackableItemConfig
    {
        protected override void Awake()
        {
            base.Awake();
            
            if (Has<ComponentPlant>()) return;
            
            Components?.Add(new ComponentPlant());
        }
    }
}