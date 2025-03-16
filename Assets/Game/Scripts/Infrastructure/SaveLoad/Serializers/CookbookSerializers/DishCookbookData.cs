using System;
using System.Collections.Generic;

namespace Tavern.Infrastructure
{
    [Serializable]
    public class DishCookbookData
    {
        public List<RecipeData> Recipes;

        public DishCookbookData(int count)
        {
            Recipes = new List<RecipeData>(count);
        }
    }

    [Serializable]
    public class RecipeData
    {
        public string Name;
        public int Stars;
    }
}