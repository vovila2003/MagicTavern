using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Cooking
{
    public class DishCookbookContext : MonoBehaviour
    {
        private DishCookbook _dishCookbook;

        [SerializeField]
        private DishRecipe[] DishRecipes;
        
        [ShowInInspector, ReadOnly]
        private IReadOnlyDictionary<string, DishRecipe> Recipes => _dishCookbook?.Recipes; 

        private void Awake()
        {
            _dishCookbook = new DishCookbook(DishRecipes);
        }

        [Button]
        public void AddRecipe(DishRecipe recipe)
        {
            bool result = _dishCookbook.AddRecipe(recipe);
            if (!result)
            {
                Debug.Log("Add recipe to cookbook: FAIL");
            }
        }

        [Button]
        public void RemoveRecipe(DishRecipe recipe)
        {
            bool result = _dishCookbook.RemoveRecipeByConfig(recipe);
            if (!result)
            {
                Debug.Log("Remove recipe from cookbook: FAIL");
            }
        }

        [Button]
        public void RemoveRecipe(string recipeName)
        {
            bool result = _dishCookbook.RemoveRecipeByName(recipeName);
            if (!result)
            {
                Debug.Log("Remove recipe from cookbook: FAIL");
            }
        }
    }
}