using System;
using Modules.Crafting;
using Tavern.Gardening;

namespace Tavern.Cooking
{
    [Serializable]
    public class ProductIngredient : Ingredient<ProductItem>
    {
        public ProductItemConfig Product => Item as ProductItemConfig;
        public int ProductAmount => ItemAmount;
    }
}