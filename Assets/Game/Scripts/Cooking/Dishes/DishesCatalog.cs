using Modules.Items;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "DishesCatalog", 
        menuName = "Settings/Cooking/Dishes/Dishes Catalog")]
    public class DishesCatalog : ItemsCatalog
    {
        public override string CatalogName => GetType().Name;
    }
}