using System;
using System.Collections.Generic;
using Tavern.ProductsAndIngredients;

namespace Tavern.Cooking
{
    public interface IActiveDishRecipeReader
    {
        event Action OnChanged;
        event Action<List<PlantProductItem>, List<AnimalProductItem>> OnSpent;
        DishRecipe Recipe { get; }
        bool HasItem(string item);
        bool IsEmpty { get; }
    }
}