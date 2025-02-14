using Modules.Items;
using Modules.Shopping;
using UnityEngine;

namespace Tavern.ProductsAndIngredients
{
    [CreateAssetMenu(
        fileName = "AnimalProduct",
        menuName = "Settings/Products/Animal Product Config")]
    public class AnimalProductItemConfig : SellableStackableItemConfig
    {
        public override Item Create()
        {
            return new AnimalProductItem(this, GetComponentClones());
        }
    }
}