using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "DishRecipeCatalog", 
        menuName = "Settings/Cooking/DishRecipes/Dish Recipe Catalog")]
    public class DishRecipeCatalog : ScriptableObject
    {
        [SerializeField] 
        private DishRecipe[] Recipes;
        
        private readonly Dictionary<string, DishRecipe> _recipes = new();
        
        public Dictionary<string, DishRecipe>.ValueCollection RecipeList => _recipes.Values;

        public bool TryGetRecipe(string recipeName, out DishRecipe recipe) => 
            _recipes.TryGetValue(recipeName, out recipe);
        
        private void OnValidate()
        {
            var collection = new Dictionary<DishRecipe, bool>();
            foreach (DishRecipe recipe in Recipes)
            {
                _recipes.Add(recipe.Name, recipe);
                if (collection.TryAdd(recipe, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate dish recipe of name {recipe.Name} in catalog");
            }
        }
    }
}