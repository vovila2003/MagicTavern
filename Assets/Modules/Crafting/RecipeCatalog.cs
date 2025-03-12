using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Crafting
{
    public class RecipeCatalog<T> : ScriptableObject where T : ItemRecipe
    {
        [SerializeField] 
        private T[] Recipes;
        
        private readonly Dictionary<string, T> _recipes = new();
        
        public Dictionary<string, T>.ValueCollection RecipeList => _recipes.Values;

        public bool TryGetRecipe(string recipeName, out T recipe) => 
            _recipes.TryGetValue(recipeName, out recipe);

        [Button]
        private void Validate()
        {
            OnValidate();
        }

        private void Awake()
        {
            _recipes.Clear();
            foreach (T recipe in Recipes)
            {
                _recipes.Add(recipe.Name, recipe);
            }
        }

        private void OnValidate()
        {
            _recipes.Clear();
            var collection = new Dictionary<T, bool>();
            foreach (T recipe in Recipes)
            {
                recipe.Validate();
                _recipes.Add(recipe.Name, recipe);
                if (collection.TryAdd(recipe, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate recipe of name {recipe.Name} in catalog");
            }
        }
    }
}