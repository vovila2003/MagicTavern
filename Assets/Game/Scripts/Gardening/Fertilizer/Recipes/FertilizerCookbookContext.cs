using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tavern.Cooking;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tavern.Gardening.Fertilizer
{
    public class FertilizerCookbookContext : MonoBehaviour
    {
        private FertilizerCookbook _fertilizerCookbook;

        [SerializeField]
        private FertilizerRecipe[] FertilizerRecipes;
        
        [ShowInInspector, ReadOnly]
        private IReadOnlyDictionary<string, FertilizerRecipe> Recipes => _fertilizerCookbook?.Recipes; 

        private void Awake()
        {
            _fertilizerCookbook = new FertilizerCookbook(FertilizerRecipes);
        }

        [Button]
        public void AddRecipe(FertilizerRecipe recipe)
        {
            bool result = _fertilizerCookbook.AddRecipe(recipe);
            if (!result)
            {
                Debug.Log("Add recipe to cookbook: FAIL");
            }
        }

        [Button]
        public void RemoveRecipe(FertilizerRecipe recipe)
        {
            bool result = _fertilizerCookbook.RemoveRecipeByConfig(recipe);
            if (!result)
            {
                Debug.Log("Remove recipe from cookbook: FAIL");
            }
        }

        [Button]
        public void RemoveRecipe(string recipeName)
        {
            bool result = _fertilizerCookbook.RemoveRecipeByName(recipeName);
            if (!result)
            {
                Debug.Log("Remove recipe from cookbook: FAIL");
            }
        }
    }
}