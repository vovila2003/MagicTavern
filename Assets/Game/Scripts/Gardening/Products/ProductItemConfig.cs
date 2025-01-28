using UnityEngine;

namespace Tavern.Gardening
{
    [CreateAssetMenu(
        fileName = "ProductConfig",
        menuName = "Settings/Gardening/Products/Product Config")]
    public class ProductItemConfig : PlantItemConfig<ProductItem>
    {
        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Item.TryGet(out ComponentPlant component)) return;
            
            if (component.Config is null) return;
            
            Item.SetName(ProductNameProvider.GetName(component.Config.Name));
        }
    }
}