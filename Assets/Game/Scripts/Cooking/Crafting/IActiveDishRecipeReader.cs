using System;
using System.Collections.Generic;
using Tavern.Gardening;
using Tavern.Looting;

namespace Tavern.Cooking
{
    public interface IActiveDishRecipeReader
    {
        event Action OnChanged;
        event Action<List<ProductItem>, List<LootItem>> OnSpent;
        DishRecipe Recipe { get; }
        bool HasItem(string item);
    }
}