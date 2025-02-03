using Modules.Items;
using UnityEngine;

namespace Tavern.ProductsAndIngredients
{
    [CreateAssetMenu(
        fileName = "ProductCatalog", 
        menuName = "Settings/Gardening/Products/Products Catalog")]
    public class PlantProductCatalog : ItemsCatalog<PlantProductItem>
    {
    }
}