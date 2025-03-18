using System;
using System.Collections.Generic;

namespace Tavern.Infrastructure
{
    [Serializable]
    public class DishAutoCookbookData
    {
        public List<string> Recipes;

        public DishAutoCookbookData(int count)
        {
            Recipes = new List<string>(count);
        }
    }
}