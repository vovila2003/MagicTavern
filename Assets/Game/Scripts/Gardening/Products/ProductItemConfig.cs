using UnityEngine;

namespace Tavern.Gardening
{
    [CreateAssetMenu(
        fileName = "ProductConfig",
        menuName = "Settings/Gardening/Products/Product Config")]
    public class ProductItemConfig : PlantItemConfig<ProductItem>
    {
        protected void OnValidate()
        {
            ProductItem productItem = GetItem();
            if (!productItem.TryGet(out ComponentPlant component)) return;
            
            if (component.Config is null) return;
            
            productItem.SetName(ProductNameProvider.GetName(component.Config.Name));
        }
    }
}