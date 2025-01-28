using UnityEngine;

namespace Tavern.Gardening
{
    [CreateAssetMenu(
        fileName = "SeedConfig",
        menuName = "Settings/Gardening/Seeds/Seed Config")]
    public class SeedItemConfig : PlantItemConfig<SeedItem>
    {
        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Item.TryGet(out ComponentPlant component)) return;
            
            if (component.Config is null) return;
            
            Item.SetName(SeedNameProvider.GetName(component.Config.Name));
        }
    }
}