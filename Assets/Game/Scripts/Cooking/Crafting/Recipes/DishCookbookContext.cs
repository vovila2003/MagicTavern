using System;
using System.Collections.Generic;
using Modules.Crafting;
using Modules.GameCycle.Interfaces;
using Tavern.Common;

namespace Tavern.Cooking
{
    public class DishCookbookContext : CookbookContext<DishItem>, IInitGameListener
    {
        private readonly Dictionary<DishRecipe, int> _recipes = new();

        void IInitGameListener.OnInit()
        {
            foreach (ItemRecipe<DishItem> recipe in Recipes.Values)
            {
                AddNewRecipe(recipe);
            }
        }

        public int GetRecipeStars(DishRecipe recipe)
        {
            return _recipes[recipe];
        }

        public void SetRecipeStars(DishRecipe recipe, int count)
        {
            _recipes[recipe] = count;
        }

        protected override void OnAddRecipe(ItemRecipe<DishItem> recipe)
        {
            AddNewRecipe(recipe);
        }

        protected override void OnRemoveRecipe(ItemRecipe<DishItem> recipe)
        {
            if (recipe is not DishRecipe dishRecipe) return;
            _recipes.Remove(dishRecipe);
        }

        private void AddNewRecipe(ItemRecipe<DishItem> recipe)
        {
            var dishRecipe = recipe as DishRecipe;
            if (dishRecipe != null)
            {
                _recipes.Add(dishRecipe, 0);
            }
            else
            {
                throw new ArgumentException($"Recipe {recipe.Name} is not a DishRecipe");
            }
        }
    }
}