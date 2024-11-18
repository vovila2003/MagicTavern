using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Cooking
{
    public class CookbookContext : MonoBehaviour
    {
        private Cookbook _cookbook;

        [SerializeField]
        private DishRecipe[] DishRecipes;
        
        [ShowInInspector, ReadOnly]
        private IReadOnlyDictionary<string, DishRecipe> Recipes => _cookbook?.Recipes; 

        private void Awake()
        {
            _cookbook = new Cookbook(DishRecipes);
        }

        [Button]
        public void AddRecipe(DishRecipe recipe)
        {
            bool result = _cookbook.AddRecipe(recipe);
            if (!result)
            {
                Debug.Log("Add recipe to cookbook: FAIL");
            }
        }

        [Button]
        public void RemoveRecipe(DishRecipe recipe)
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