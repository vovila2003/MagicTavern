using Tavern.Gardening;
using UnityEngine;

namespace Tavern.ProductsAndIngredients
{
    [CreateAssetMenu(
        fileName = "Product",
        menuName = "Settings/Gardening/Products/Product Config")]
    public class PlantProductItemConfig : PlantItemConfig<PlantProductItem>
    {
        protected void OnValidate()
        {
            PlantProductItem plantProductItem = GetItem();
            if (!plantProductItem.TryGet(out ComponentPlant component)) return;
            
            if (component.Config is null) return;
            
            plantProductItem.SetName(ProductNameProvider.GetName(component.Config.Name));
        }
    }
}