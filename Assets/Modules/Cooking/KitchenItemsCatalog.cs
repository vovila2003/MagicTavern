using Modules.Items;
using UnityEngine;

namespace Modules.Cooking
{
    [CreateAssetMenu(
        fileName = "KitchenItemsCatalog", 
        menuName = "Settings/Cooking/KitchenItems Catalog")]
    public class KitchenItemsCatalog : ItemsCatalog<KitchenItem>
    {
    }
}