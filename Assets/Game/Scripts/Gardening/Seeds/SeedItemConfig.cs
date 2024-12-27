using UnityEngine;

namespace Tavern.Gardening
{
    [CreateAssetMenu(
        fileName = "SeedConfig",
        menuName = "Settings/Gardening/Seeds/Seed Config")]
    public class SeedItemConfig : PlantItemConfig<SeedItem>
    {
        private void OnValidate()
        {
            if (!Item.TryGetComponent(out ComponentPlant component)) return;
            
            if (component.Config is null) return;
            
            Item.SetName(SeedNameProvider.GetName(component.Config.Name));
        }
    }
}