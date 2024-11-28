using System.Collections.Generic;
using Modules.Items;

namespace Modules.Crafting
{
    public class Cookbook<T> where T : Item
    {
        private readonly Dictionary<string, ItemRecipe<T>> _recipes = new();
        private readonly Dictionary<ItemConfig<T>, ItemRecipe<T>> _recipesByConfig = new();

        public IReadOnlyDictionary<string, ItemRecipe<T>> Recipes => _recipes;

        public Cookbook(ItemRecipe<T>[] recipes)
        {
            foreach (ItemRecipe<T> recipe in recipes)
            {
                _recipes[recipe.Name] = recipe;
                _recipesByConfig[recipe.ResultItem] = recipe;
            }
        }

        public bool TryGetRecipeByConfig(ItemConfig<T> itemConfig, out ItemRecipe<T> recipe)
        {
            return _recipesByConfig.TryGetValue(itemConfig, out recipe);
        }

        public bool TryGetRecipeByName(string recipeName, out ItemRecipe<T> recipe)
        {
            return _recipes.TryGetValue(recipeName, out recipe);
        }

        public bool AddRecipe(ItemRecipe<T> recipe)
        {
            return _recipes.TryAdd(recipe.Name, recipe) && 
                   _recipesByConfig.TryAdd(recipe.ResultItem, recipe);
        }

        public bool RemoveRecipeByConfig(ItemRecipe<T> recipe)
        {
            return _recipes.Remove(recipe.Name) &&
                   _recipesByConfig.Remove(recipe.ResultItem);
        }
        
        public bool RemoveRecipeByName(string name)
        {
            return _recipes.Remove(name, out ItemRecipe<T> recipe) &&
                   _recipesByConfig.Remove(recipe.ResultItem);
        }
    }
}