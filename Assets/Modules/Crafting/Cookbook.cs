using System;
using System.Collections.Generic;
using Modules.Items;

namespace Modules.Crafting
{
    public class Cookbook
    {
        public event Action<ItemRecipe> OnRecipeAdded;
        public event Action<ItemRecipe> OnRecipeRemoved;
        
        private readonly Dictionary<string, ItemRecipe> _recipes = new();
        private readonly Dictionary<ItemConfig, ItemRecipe> _recipesByConfig = new();

        public IReadOnlyDictionary<string, ItemRecipe> Recipes => _recipes;

        public Cookbook(ItemRecipe[] recipes)
        {
            foreach (ItemRecipe recipe in recipes)
            {
                _recipes[recipe.Name] = recipe;
                _recipesByConfig[recipe.ResultItemConfig] = recipe;
            }
        }

        public bool TryGetRecipeByConfig(ItemConfig itemConfig, out ItemRecipe recipe)
        {
            return _recipesByConfig.TryGetValue(itemConfig, out recipe);
        }

        public bool TryGetRecipeByName(string recipeName, out ItemRecipe recipe)
        {
            return _recipes.TryGetValue(recipeName, out recipe);
        }

        public bool AddRecipe(ItemRecipe recipe)
        {
            bool result = _recipes.TryAdd(recipe.Name, recipe) 
                && _recipesByConfig.TryAdd(recipe.ResultItemConfig, recipe);
            if (result)
            {
                OnRecipeAdded?.Invoke(recipe);    
            }
            
            return result;
        }

        public bool RemoveRecipeByConfig(ItemRecipe recipe)
        {
            bool result = _recipes.Remove(recipe.Name) && _recipesByConfig.Remove(recipe.ResultItemConfig);
            if (result)
            {
                OnRecipeRemoved?.Invoke(recipe);    
            }
            
            return result;
        }
        
        public bool RemoveRecipeByName(string name)
        {
            bool result = _recipes.Remove(name, out ItemRecipe recipe) 
                && _recipesByConfig.Remove(recipe.ResultItemConfig);
            if (result)
            {
                OnRecipeRemoved?.Invoke(recipe);      
            }
            
            return result;
        }

        public void Clear()
        {
            foreach (ItemRecipe recipe in Recipes.Values)
            {
                RemoveRecipeByConfig(recipe);
            }
            
            _recipes.Clear();
            _recipesByConfig.Clear();
        }
    }
}