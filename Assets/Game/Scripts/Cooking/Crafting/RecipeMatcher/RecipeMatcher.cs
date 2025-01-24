using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Modules.Items;
using Tavern.Gardening;
using Tavern.Looting;
using UnityEngine;

namespace Tavern.Cooking
{
    [UsedImplicitly]
    public class RecipeMatcher : IInitGameListener, IExitGameListener
    {
        public event Action<DishRecipe> OnRecipeMatched;
        
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
        private readonly ActiveDishRecipe _recipe;
        private readonly DishCookbookContext _cookbook;
        private readonly DictionaryComparer _comparer;
        private Dictionary<Dictionary<string, int>, DishRecipe> _recipes;
        private readonly List<string> _recipeNames = new();

        public RecipeMatcher(
            DishRecipeCatalog recipeCatalog, 
            ActiveDishRecipe recipe, 
            DishCookbookContext cookbook)
        {
            _recipeCatalog = recipeCatalog;
            _recipe = recipe;
            _cookbook = cookbook;
            _comparer = new DictionaryComparer();
        }

        void IInitGameListener.OnInit()
        {
            _recipe.OnChanged += OnRecipeChanged;
        }

        void IExitGameListener.OnExit()
        {
            _recipe.OnChanged -= OnRecipeChanged;
        }

        private void OnRecipeChanged()
        {
            _recipeNames.Clear();
            AddNames(_recipe.Products);
            AddNames(_recipe.FakeProducts);
            AddNames(_recipe.Loots);
            AddNames(_recipe.FakeLoots);
            if (!MatchRecipe(out DishRecipe recipe)) return;
            
            OnRecipeMatched?.Invoke(recipe);
            _cookbook.AddRecipe(recipe);
        }

        private void AddNames(IReadOnlyList<Item> collection)
        {
            foreach (Item item in collection)
            {
                _recipeNames.Add(item.ItemName);
            }
        }

        private bool MatchRecipe(out DishRecipe resultRecipe)
        {
            if (_recipes is null) Init();

            var key = new Dictionary<string, int>();
            foreach (string item in _recipeNames)
            {
                AddToDictionary(key, item);
            }

            resultRecipe = null;
            if (_recipes is null) return false;
            
            bool result = _recipes.TryGetValue(key, out DishRecipe recipe);
            resultRecipe = recipe;
            
            return result;
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