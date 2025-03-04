using System.Text;
using Modules.Gardening;
using Modules.Inventories;
using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening.Medicine
{
    [CreateAssetMenu(
        fileName = "MedicineConfig",
        menuName = "Settings/Gardening/Medicine/Medicine Config")]
    public class MedicineConfig : StackableItemConfig
    {
        public override Item Create()
        {
            return new MedicineItem(this, GetComponentClones());
        }

        protected override string GetItemType() => nameof(MedicineItem);

        protected override void Awake()
        {
            base.Awake();

            SetFlags(ItemFlags.Consumable);

            if (!Has<ComponentHarvestHeal>())
            {
                Components?.Add(new ComponentHarvestHeal());
            }

            if (!Has<ComponentHarvestSicknessReducing>())
            {
                Components?.Add(new ComponentHarvestSicknessReducing());
            }
        }
        
        public override string Description => GetDescription();

        private string GetDescription()
        {
            var builder = new StringBuilder();
            builder.AppendLine(base.Description);
            if (TryGet(out ComponentHarvestSicknessReducing component))
            {
                builder.AppendLine($"Снижение вероятности заболевания: {component.Reducing};");
            }

            return builder.ToString();
        }
    }
}