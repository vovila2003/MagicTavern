using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Items;
using Tavern.ProductsAndIngredients;
using Tavern.Settings;
using UnityEngine;

namespace Tavern.Cooking
{
    [UsedImplicitly]
    public class RecipeMatcher
    {
        private class DictionaryComparer : EqualityComparer<Dictionary<string, int>>
        {
            public override bool Equals(Dictionary<string, int> x, Dictionary<string, int> y)
            {
                if (x == null && y == null) return true;
                
                if (x == null || y == null) return false;
                
                if (x.Count != y.Count) return false;

                foreach (KeyValuePair<string,int> pair in x)
                {
                    if (!y.TryGetValue(pair.Key, out int value)) return false;
                    
                    if (pair.Value != value) return false;
                }
                
                return true;
            }

            public override int GetHashCode(Dictionary<string, int> dict)
            {
                var hash = 0;
                foreach (KeyValuePair<string, int> pair in dict)
                {
                    if (pair.Value != 0)
                    {
                        hash = hash ^ pair.Key.GetHashCode() ^ pair.Value;
                    }
                }

                return hash;
            }
        }

        private readonly DishRecipeCatalog _recipeCatalog;
        private readonly DictionaryComparer _comparer;
        private Dictionary<Dictionary<string, int>, DishRecipe> _recipes;
        private KitchenItemConfig _kitchen;

        public RecipeMatcher(CookingSettings settings)
        {
            _recipeCatalog = settings.DishRecipes;
            _comparer = new DictionaryComparer();
        }

        public bool MatchRecipe(ActiveDishRecipe activeRecipe, out DishRecipe resultRecipe)
        {
            resultRecipe = null;
            if (activeRecipe is null) return false;

            if (_kitchen != activeRecipe.RequiredKitchen || _recipes is null)
            {
                Init(activeRecipe);
            } 
            
            if (activeRecipe.FakePlantProducts.Count != 0 || activeRecipe.FakeAnimalProducts.Count != 0) return false;

            Dictionary<string, int> key = GetKey(activeRecipe);

            bool result = _recipes!.TryGetValue(key, out resultRecipe);
            
            return result;
        }

        private static Dictionary<string, int> GetKey(ActiveDishRecipe activeRecipe)
        {
            var key = new Dictionary<string, int>();
            foreach (Item item in activeRecipe.PlantProducts)
            {
                AddToDictionary(key, item.ItemName);
            }
            
            foreach (Item item in activeRecipe.AnimalProducts)
            {
                AddToDictionary(key, item.ItemName);
            }

            return key;
        }

        private void Init(ActiveDishRecipe activeRecipe)
        {
            _kitchen = activeRecipe.RequiredKitchen;
            _recipes = new Dictionary<Dictionary<string, int>, DishRecipe>(_comparer);
            Dictionary<string, DishRecipe>.ValueCollection recipeList = _recipeCatalog.RecipeList;
            foreach (DishRecipe recipe in recipeList)
            {
                if (recipe.KitchenItem != _kitchen) continue;
                
                var recipeDict = new Dictionary<string, int>();
                foreach (PlantProductItemConfig plantProduct in recipe.PlantProducts)
                {
                    AddToDictionary(recipeDict, plantProduct.GetItem().ItemName);
                }

                foreach (AnimalProductItemConfig animalProduct in recipe.AnimalProducts)
                {
                    AddToDictionary(recipeDict, animalProduct.GetItem().ItemName);
                }

                if (!_recipes.TryAdd(recipeDict, recipe))
                {
                    Debug.LogWarning("Can't add recipeDict to collection in RecipeMatcher.Init()");
                }
            }
        }

        private static void AddToDictionary(Dictionary<string, int> recipeDict, string itemName)
        {
            recipeDict.TryAdd(itemName, 0);
            recipeDict[itemName]++;
        }
    }
}