using System.Collections.Generic;
using JetBrains.Annotations;
using Tavern.Gardening;
using Tavern.Looting;
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

        public RecipeMatcher(DishRecipeCatalog recipeCatalog)
        {
            _recipeCatalog = recipeCatalog;
            _comparer = new DictionaryComparer();
        }

        public (bool, DishRecipe) MatchRecipe(IReadOnlyList<string> items)
        {
            if (_recipes is null) Init();

            var key = new Dictionary<string, int>();
            foreach (string item in items)
            {
                AddToDictionary(key, item);
            }

            if (_recipes is null) return (false, null);
            
            bool result = _recipes.TryGetValue(key, out DishRecipe recipe);

            return (result, recipe);
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
                    AddToDictionary(recipeDict, product.Item.ItemName);
                }

                foreach (LootItemConfig loot in recipe.Loots)
                {
                    AddToDictionary(recipeDict, loot.Item.ItemName);
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