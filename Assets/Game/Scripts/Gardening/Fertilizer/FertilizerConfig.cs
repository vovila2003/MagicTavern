using Modules.Inventories;
using Modules.Items;
using Tavern.Gardening.Medicine;
using UnityEngine;

namespace Tavern.Gardening.Fertilizer
{
    [CreateAssetMenu(
        fileName = "FertilizerConfig",
        menuName = "Settings/Gardening/Fertilizer/Fertilizer Config")]
    public class FertilizerConfig : StackableItemConfig
    {
        public override Item Create()
        {
            return new FertilizerItem(this, GetComponentClones());
        }

        protected override void Awake()
        {
            base.Awake();
            SetFlags(ItemFlags.Consumable);

            if (!Has<ComponentHarvestBooster>())
            {
                Components?.Add(new ComponentHarvestBooster());    
            }
            
            if (!Has<ComponentHarvestSicknessReducing>())
            {
                Components?.Add(new ComponentHarvestSicknessReducing());    
            }
            
            if (!Has<ComponentGrowthAcceleration>())
            {
                Components?.Add(new ComponentGrowthAcceleration());    
            }
        }
    }
}