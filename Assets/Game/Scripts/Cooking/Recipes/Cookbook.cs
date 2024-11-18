using System.Collections.Generic;

namespace Tavern.Cooking
{
    public class Cookbook
    {
        private readonly Dictionary<string, DishRecipe> _recipes = new();
        private readonly Dictionary<DishItemConfig, DishRecipe> _recipesByConfig = new();

        public IReadOnlyDictionary<string, DishRecipe> Recipes => _recipes;

        public Cookbook(DishRecipe[] recipes)
        {
            foreach (DishRecipe recipe in recipes)
            {
                if (recipe.ResultItem is not DishItemConfig config) continue;
                
                _recipes[recipe.Name] = recipe;
                _recipesByConfig[config] = recipe;
            }
        }

        public bool TryGetRecipeByConfig(DishItemConfig dishItemConfig, out DishRecipe recipe)
        {
            return _recipesByConfig.TryGetValue(dishItemConfig, out recipe);
        }

        public bool TryGetRecipeByName(string recipeName, out DishRecipe recipe)
        {
            return _recipes.TryGetValue(recipeName, out recipe);
        }

        public bool AddRecipe(DishRecipe recipe)
        {
            return _recipes.TryAdd(recipe.Name, recipe) && 
                   _recipesByConfig.TryAdd(recipe.ResultItem as DishItemConfig, recipe);
        }

        public bool RemoveRecipeByConfig(DishRecipe recipe)
        {
            return recipe.ResultItem is DishItemConfig config &&
                   _recipes.Remove(recipe.Name) &&
                   _recipesByConfig.Remove(config);
        }
        
        public bool RemoveRecipeByName(string name)
        {
            return _recipes.Remove(name, out DishRecipe recipe) &&
                   recipe.ResultItem is DishItemConfig config && 
                   _recipesByConfig.Remove(config);
        }
    }
}