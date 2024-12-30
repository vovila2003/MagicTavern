using UnityEngine;

namespace Tavern.Gardening
{
    [CreateAssetMenu(
        fileName = "ProductConfig",
        menuName = "Settings/Gardening/Products/Product Config")]
    public class ProductItemConfig : PlantItemConfig<ProductItem>
    {
        private void OnValidate()
        {
            if (!Item.TryGetComponent(out ComponentPlant component)) return;
            
            if (component.Config is null) return;
            
            Item.SetName(ProductNameProvider.GetName(component.Config.Name));
        }
    }
}