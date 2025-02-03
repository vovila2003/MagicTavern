using Modules.Inventories;
using UnityEngine;

namespace Tavern.ProductsAndIngredients
{
    [CreateAssetMenu(
        fileName = "AnimalProduct",
        menuName = "Settings/Products/Animal Product Config")]
    public class AnimalProductItemConfig : StackableItemConfig<AnimalProductItem>
    {
    }
}