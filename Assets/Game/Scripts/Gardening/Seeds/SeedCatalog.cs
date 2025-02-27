using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening
{
    [CreateAssetMenu(
        fileName = "SeedsCatalog", 
        menuName = "Settings/Gardening/Seeds/Seeds Catalog")]
    public class SeedCatalog : ItemsCatalog
    {
        public override string CatalogName => GetType().Name;
    }
}