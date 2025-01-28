using UnityEngine;

namespace Tavern.Gardening
{
    [CreateAssetMenu(
        fileName = "SeedConfig",
        menuName = "Settings/Gardening/Seeds/Seed Config")]
    public class SeedItemConfig : PlantItemConfig<SeedItem>
    {
        protected void OnValidate()
        {
            SeedItem seedItem = GetItem();
            if (!seedItem.TryGet(out ComponentPlant component)) return;
            
            if (component.Config is null) return;
            
            seedItem.SetName(SeedNameProvider.GetName(component.Config.Name));
        }
    }
}