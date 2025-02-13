using Tavern.ProductsAndIngredients;
using UnityEngine;

namespace Tavern.Shopping
{
    [CreateAssetMenu
    (fileName = "ProductSellerConfig",
        menuName = "Settings/Shopping/ProductSellerConfig")]
    public class ProductSellerConfig : SellerConfig<ProductItem>
    {
    }
}