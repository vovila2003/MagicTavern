using System;
using System.Collections.Generic;
using Modules.Crafting;
using Tavern.Common;

namespace Tavern.Cooking
{
    public class DishCookbookContext : CookbookContext
    {
        private const int StartStarsCount = 1;
        private const int ValueInStar = 2;
        
        public event Action<DishRecipe, int> OnStarsChanged;
        
        private readonly Dictionary<DishRecipe, int> _recipeStars = new();

        protected override void OnAwake()
        {
            base.OnAwake();
            foreach (ItemRecipe recipe in Recipes.Values)
            {
                AddNewRecipe(recipe);
            }
        }

        public int GetRecipeStars(DishRecipe recipe)
        {
            return _recipeStars[recipe];
        }

        public void SetRecipeStars(DishRecipe recipe, int count)
        {
            _recipeStars[recipe] = count;
            OnStarsChanged?.Invoke(recipe, count);
        }

        protected override void OnAddRecipe(ItemRecipe recipe)
        {
            AddNewRecipe(recipe);
        }

        protected override void OnRemoveRecipe(ItemRecipe recipe)
        {
            if (recipe is not DishRecipe dishRecipe) return;
            _recipeStars.Remove(dishRecipe);
        }

        private void AddNewRecipe(ItemRecipe recipe)
        {
            if (recipe is not DishRecipe dishRecipe)
                throw new ArgumentException($"Recipe {recipe.Name} is not a DishRecipe");
            
            _recipeStars.TryAdd(dishRecipe, StartStarsCount * ValueInStar);
        }
    }
}