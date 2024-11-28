using System.Collections.Generic;

namespace Tavern.Gardening.Fertilizer
{
    public class FertilizerCookbook
    {
        private readonly Dictionary<string, FertilizerRecipe> _recipes = new();
        private readonly Dictionary<FertilizerConfig, FertilizerRecipe> _recipesByConfig = new();

        public IReadOnlyDictionary<string, FertilizerRecipe> Recipes => _recipes;

        public FertilizerCookbook(FertilizerRecipe[] recipes)
        {
            foreach (FertilizerRecipe recipe in recipes)
            {
                if (recipe.ResultItem is not FertilizerConfig config) continue;
                
                _recipes[recipe.Name] = recipe;
                _recipesByConfig[config] = recipe;
            }
        }

        public bool TryGetRecipeByConfig(FertilizerConfig dishItemConfig, out FertilizerRecipe recipe)
        {
            return _recipesByConfig.TryGetValue(dishItemConfig, out recipe);
        }

        public bool TryGetRecipeByName(string recipeName, out FertilizerRecipe recipe)
        {
            return _recipes.TryGetValue(recipeName, out recipe);
        }

        public bool AddRecipe(FertilizerRecipe recipe)
        {
            return _recipes.TryAdd(recipe.Name, recipe) && 
                   _recipesByConfig.TryAdd(recipe.ResultItem as FertilizerConfig, recipe);
        }

        public bool RemoveRecipeByConfig(FertilizerRecipe recipe)
        {
            return recipe.ResultItem is FertilizerConfig config &&
                   _recipes.Remove(recipe.Name) &&
                   _recipesByConfig.Remove(config);
        }
        
        public bool RemoveRecipeByName(string name)
        {
            return _recipes.Remove(name, out FertilizerRecipe recipe) &&
                   recipe.ResultItem is FertilizerConfig config && 
                   _recipesByConfig.Remove(config);
        }
    }
}