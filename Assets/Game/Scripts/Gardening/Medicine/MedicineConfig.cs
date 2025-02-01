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

            MedicineItem medicineItem = GetItem();
            medicineItem.SetFlags(ItemFlags.Consumable);

            if (!medicineItem.Has<ComponentHarvestHeal>())
            {
                medicineItem.Components?.Add(new ComponentHarvestHeal());
            }

            if (!medicineItem.Has<ComponentHarvestSicknessReducing>())
            {
                medicineItem.Components?.Add(new ComponentHarvestSicknessReducing());
            }
        }
    }
}