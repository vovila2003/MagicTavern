using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening
{
    [CreateAssetMenu(
        fileName = "ProductCatalog", 
        menuName = "Settings/Gardening/Products/Products Catalog")]
    public class ProductCatalog : ItemsCatalog<ProductItem>
    {
    }
}