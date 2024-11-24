using Modules.Inventories;
using Modules.Items;
using Tavern.Gardening.Medicine;
using UnityEngine;

namespace Tavern.Gardening.Fertilizer
{
    [CreateAssetMenu(
        fileName = "FertilizerConfig",
        menuName = "Settings/Gardening/Fertilizer/Fertilizer Config")]
    public class FertilizerConfig : StackableItemConfig<FertilizerItem>
    {
        protected override void Awake()
        {
            base.Awake();
            Item.SetFlags(ItemFlags.Consumable);

            if (!Item.HasComponent<ComponentHarvestBooster>())
            {
                Item.Components?.Add(new ComponentHarvestBooster());    
            }
            
            if (!Item.HasComponent<ComponentHarvestSicknessReducing>())
            {
                Item.Components?.Add(new ComponentHarvestSicknessReducing());    
            }
            
            if (!Item.HasComponent<ComponentGrowthAcceleration>())
            {
                Item.Components?.Add(new ComponentGrowthAcceleration());    
            }
        }
    }
}