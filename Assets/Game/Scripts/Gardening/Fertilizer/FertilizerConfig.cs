using System;
using System.Text;
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
            return new FertilizerItem(this, GetComponentClones(), Array.Empty<IExtraItemComponent>());
        }

        protected override string GetItemType() => nameof(FertilizerItem);

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
        
        public override string Description => GetDescription();

        private string GetDescription()
        {
            var builder = new StringBuilder();
            builder.AppendLine(base.Description);
            if (TryGet(out ComponentHarvestBooster booster))
            {
                builder.AppendLine($"Увеличение урожайности: {booster.Boost}%;");
            }
            
            if (TryGet(out ComponentHarvestSicknessReducing reducing))
            {
                builder.AppendLine($"Уменьшение веростности заболевания: {reducing.Reducing}%;");
            }
            
            if (TryGet(out ComponentGrowthAcceleration acceleration))
            {
                builder.AppendLine($"Ускорение роста: {acceleration.Acceleration}%;");
            }

            return builder.ToString();
        }
    }
}