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

            if (!Item.Has<ComponentHarvestBooster>())
            {
                Item.Components?.Add(new ComponentHarvestBooster());    
            }
            
            if (!Item.Has<ComponentHarvestSicknessReducing>())
            {
                Item.Components?.Add(new ComponentHarvestSicknessReducing());    
            }
            
            if (!Item.Has<ComponentGrowthAcceleration>())
            {
                Item.Components?.Add(new ComponentGrowthAcceleration());    
            }
        }
    }
}