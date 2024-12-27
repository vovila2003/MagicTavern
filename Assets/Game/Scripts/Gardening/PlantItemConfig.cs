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

        private void OnValidate()
        {
            if (!Item.TryGetComponent(out ComponentPlant component)) return;
            
            if (component.Config is null) return;
            
            Item.SetName(component.Config.Name);
        }
    }
}