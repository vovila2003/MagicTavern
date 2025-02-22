using Modules.Items;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "KitchenItemsCatalog", 
        menuName = "Settings/Cooking/KitchenItems/Kitchen Items Catalog")]
    public class KitchenItemsCatalog : ItemsCatalog
    {
        public override string CatalogName => GetType().Name;
    }
}