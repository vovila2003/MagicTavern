using System;
using System.Collections.Generic;
using Modules.Items;

namespace Modules.Crafting
{
    public class Cookbook<T> where T : Item
    {
        public event Action<ItemRecipe<T>> OnRecipeAdded;
        public event Action<ItemRecipe<T>> OnRecipeRemoved;
        
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
            bool result = _recipes.TryAdd(recipe.Name, recipe) && _recipesByConfig.TryAdd(recipe.ResultItem, recipe);
            if (result)
            {
                OnRecipeAdded?.Invoke(recipe);    
            }
            
            return result;
        }

        public bool RemoveRecipeByConfig(ItemRecipe<T> recipe)
        {
            bool result = _recipes.Remove(recipe.Name) && _recipesByConfig.Remove(recipe.ResultItem);
            if (result)
            {
                OnRecipeRemoved?.Invoke(recipe);    
            }
            
            return result;
        }
        
        public bool RemoveRecipeByName(string name)
        {
            bool result = _recipes.Remove(name, out ItemRecipe<T> recipe) && _recipesByConfig.Remove(recipe.ResultItem);
            if (result)
            {
                OnRecipeRemoved?.Invoke(recipe);      
            }
            
            return result;
        }
    }
}