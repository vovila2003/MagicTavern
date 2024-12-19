using System.Collections.Generic;
using Modules.Crafting;
using Modules.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Common
{
    public class CookbookContext<T> : MonoBehaviour where T : Item
    {
        private Cookbook<T> _cookbook;

        [SerializeField]
        private ItemRecipe<T>[] ItemRecipes;
        
        [ShowInInspector, ReadOnly]
        private IReadOnlyDictionary<string, ItemRecipe<T>> Recipes => _cookbook?.Recipes; 
        
        private void Awake()
        {
            _cookbook = new Cookbook<T>(ItemRecipes);
        }

        public bool HasRecipe(ItemRecipe<T> recipe) => 
            _cookbook?.Recipes.ContainsKey(recipe.Name) ?? false;

        [Button]
        public void AddRecipe(ItemRecipe<T> recipe)
        {
            bool result = _cookbook.AddRecipe(recipe);
            if (!result)
            {
                Debug.Log("Add recipe to cookbook: FAIL");
            }
        }

        [Button]
        public void RemoveRecipe(ItemRecipe<T> recipe)
        {
            bool result = _cookbook.RemoveRecipeByConfig(recipe);
            if (!result)
            {
                Debug.Log("Remove recipe from cookbook: FAIL");
            }
        }

        [Button]
        public void RemoveRecipe(string recipeName)
        {
            bool result = _cookbook.RemoveRecipeByName(recipeName);
            if (!result)
            {
                Debug.Log("Remove recipe from cookbook: FAIL");
            }
        }
    }
}