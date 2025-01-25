using System;
using System.Collections.Generic;
using Modules.Crafting;
using Tavern.Common;

namespace Tavern.Cooking
{
    public class DishCookbookContext : CookbookContext<DishItem>
    {
        public event Action<DishRecipe, int> OnStarsChanged;
        
        private readonly Dictionary<DishRecipe, int> _recipes = new();

        protected override void OnAwake()
        {
            base.OnAwake();
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
            OnStarsChanged?.Invoke(recipe, count);
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
            if (recipe is not DishRecipe dishRecipe)
                throw new ArgumentException($"Recipe {recipe.Name} is not a DishRecipe");
            
            _recipes.TryAdd(dishRecipe, 0);
        }
    }
}