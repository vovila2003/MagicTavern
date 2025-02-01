using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Items;
using Tavern.Gardening;
using Tavern.Looting;
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

        public RecipeMatcher(CookingSettings settings)
        {
            _recipeCatalog = settings.DishRecipes;
            _comparer = new DictionaryComparer();
        }

        public bool MatchRecipe(ActiveDishRecipe activeRecipe, out DishRecipe resultRecipe)
        {
            if (_recipes is null) Init();
            
            resultRecipe = null;
            if (_recipes is null) return false;

            if (activeRecipe is null) return false;
            
            if (activeRecipe.FakeProducts.Count != 0 || activeRecipe.FakeLoots.Count != 0) return false;

            Dictionary<string, int> key = GetKey(activeRecipe);

            bool result = _recipes.TryGetValue(key, out resultRecipe);
            
            return result;
        }

        private static Dictionary<string, int> GetKey(ActiveDishRecipe activeRecipe)
        {
            var key = new Dictionary<string, int>();
            foreach (Item item in activeRecipe.Products)
            {
                AddToDictionary(key, item.ItemName);
            }
            
            foreach (Item item in activeRecipe.Loots)
            {
                AddToDictionary(key, item.ItemName);
            }

            return key;
        }

        private void Init()
        {
            _recipes = new Dictionary<Dictionary<string, int>, DishRecipe>(_comparer);
            Dictionary<string, DishRecipe>.ValueCollection recipeList = _recipeCatalog.RecipeList;
            foreach (DishRecipe recipe in recipeList)
            {
                var recipeDict = new Dictionary<string, int>();
                foreach (ProductItemConfig product in recipe.Products)
                {
                    AddToDictionary(recipeDict, product.GetItem().ItemName);
                }

                foreach (LootItemConfig loot in recipe.Loots)
                {
                    AddToDictionary(recipeDict, loot.GetItem().ItemName);
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