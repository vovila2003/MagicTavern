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
            FertilizerItem fertilizerItem = GetItem();
            fertilizerItem.SetFlags(ItemFlags.Consumable);

            if (!fertilizerItem.Has<ComponentHarvestBooster>())
            {
                fertilizerItem.Components?.Add(new ComponentHarvestBooster());    
            }
            
            if (!fertilizerItem.Has<ComponentHarvestSicknessReducing>())
            {
                fertilizerItem.Components?.Add(new ComponentHarvestSicknessReducing());    
            }
            
            if (!fertilizerItem.Has<ComponentGrowthAcceleration>())
            {
                fertilizerItem.Components?.Add(new ComponentGrowthAcceleration());    
            }
        }
    }
}