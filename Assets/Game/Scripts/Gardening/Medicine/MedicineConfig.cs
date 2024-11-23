using Modules.Inventories;
using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening.Medicine
{
    [CreateAssetMenu(
        fileName = "MedicineConfig",
        menuName = "Settings/Gardening/Medicine/Medicine Config")]
    public class MedicineConfig : StackableItemConfig<MedicineItem>
    {
        protected override void Awake()
        {
            base.Awake();
            if (Item.HasComponent<ComponentHarvestSickProbabilityReducing>()) return;
            
            Item.Components?.Add(new ComponentHarvestSickProbabilityReducing());
            Item.SetFlags(ItemFlags.Consumable);
        }
    }
}