using Modules.Items;
using UnityEngine;

namespace Tavern.ProductsAndIngredients
{
    [CreateAssetMenu(
        fileName = "AnimalProductCatalog", 
        menuName = "Settings/Products/Animal Products Catalog")]
    public class AnimalProductCatalog : ItemsCatalog
    {
        public override string CatalogName => GetType().Name;
    }
}