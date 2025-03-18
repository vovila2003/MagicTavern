using System;
using System.Text;
using Modules.Gardening;
using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening
{
    [CreateAssetMenu(
        fileName = "SeedConfig",
        menuName = "Settings/Gardening/Seeds/Seed Config")]
    public class SeedItemConfig : PlantItemConfig
    {
        public override void OnValidate()
        {
            base.OnValidate();
            if (!TryGet(out ComponentPlant component)) return;
            
            if (component.Config is null) return;
            
            SetName(SeedNameProvider.GetName(component.Config.Name));
        }

        public override Item Create()
        {
            return new SeedItem(this, GetComponentClones(), Array.Empty<IExtraItemComponent>());
        }

        protected override string GetItemType() => nameof(SeedItem);

        public override string Description => GetDescription();

        private string GetDescription()
        {
            var builder = new StringBuilder();
            builder.AppendLine(base.Description);
            if (TryGet(out ComponentPlant componentPlant))
            {
                Plant plant = componentPlant.Config.Plant;
                builder.AppendLine($"Урожайность: {plant.ResultValue};");
                if (plant.CanHaveSeed)
                {
                    builder.AppendLine("Семечка в урожае;");
                }
                builder.AppendLine($"Количество поливов: {plant.WateringAmount};");
                builder.AppendLine($"Вероятность заболевания: {plant.SicknessProbability};");
            }

            return builder.ToString();
        }
    }
}