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
            Item.SetFlags(ItemFlags.Consumable);

            if (!Item.HasComponent<ComponentHarvestHeal>())
            {
                Item.Components?.Add(new ComponentHarvestHeal());
            }

            if (!Item.HasComponent<ComponentHarvestSicknessReducing>())
            {
                Item.Components?.Add(new ComponentHarvestSicknessReducing());
            }
        }
    }
}