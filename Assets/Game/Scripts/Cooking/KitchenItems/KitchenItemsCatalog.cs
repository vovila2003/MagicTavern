using Modules.Items;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "KitchenItemsCatalog", 
        menuName = "Settings/Cooking/KitchenItems Catalog")]
    public class KitchenItemsCatalog : ItemsCatalog<KitchenItem>
    {
    }
}