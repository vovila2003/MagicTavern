using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening
{
    [CreateAssetMenu(
        fileName = "SeedConfig",
        menuName = "Settings/Gardening/Seeds/Seed Config")]
    public class SeedItemConfig : PlantItemConfig
    {
        protected override void OnValidate()
        {
            base.OnValidate();
            if (!TryGet(out ComponentPlant component)) return;
            
            if (component.Config is null) return;
            
            SetName(SeedNameProvider.GetName(component.Config.Name));
        }

        public override Item Create()
        {
            return new SeedItem(this, GetComponentClones());
        }

        protected override string GetItemType() => nameof(SeedItem);
    }
}