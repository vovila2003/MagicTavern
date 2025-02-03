using Modules.Items;
using UnityEngine;

namespace Tavern.ProductsAndIngredients
{
    [CreateAssetMenu(
        fileName = "PlantProductCatalog", 
        menuName = "Settings/Products/Plant Products Catalog")]
    public class PlantProductCatalog : ItemsCatalog<PlantProductItem>
    {
    }
}