using System;
using System.Collections.Generic;
using Modules.Crafting;
using Modules.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Common
{
    public class CookbookContext : MonoBehaviour
    {
        public event Action<ItemRecipe> OnRecipeAdded;
        public event Action<ItemRecipe> OnRecipeRemoved;
        
        private Cookbook _cookbook;

        [SerializeField]
        private ItemRecipe[] ItemRecipes;
        
        [ShowInInspector, ReadOnly]
        public IReadOnlyDictionary<string, ItemRecipe> Recipes => _cookbook?.Recipes; 
        
        private void Awake()
        {
            _cookbook = new Cookbook(ItemRecipes);
            OnAwake();
        }

        protected virtual void OnAwake() { }

        private void OnEnable()
        {
            _cookbook.OnRecipeAdded += OnAdded;    
            _cookbook.OnRecipeRemoved += OnRemoved;    
        }

        private void OnDisable()
        {
            _cookbook.OnRecipeAdded -= OnAdded;    
            _cookbook.OnRecipeRemoved -= OnRemoved;
        }

        public bool HasRecipe(ItemRecipe recipe) => 
            _cookbook?.Recipes.ContainsKey(recipe.Name) ?? false;
        
        public bool TryGetRecipeByConfig(ItemConfig itemConfig, out ItemRecipe recipe) => 
            _cookbook.TryGetRecipeByConfig(itemConfig, out recipe);

        public bool TryGetRecipeByName(string recipeName, out ItemRecipe recipe) => 
            _cookbook.TryGetRecipeByName(recipeName, out recipe);

        [Button]
        public void AddRecipe(ItemRecipe recipe)
        {
            bool result = _cookbook.AddRecipe(recipe);
            if (!result)
            {
                Debug.Log("Add recipe to cookbook: FAIL");
            }
        }

        [Button]
        public void RemoveRecipe(ItemRecipe recipe)
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

        [Button]
        public void Clear()
        {
            _cookbook.Clear();
        }

        private void OnAdded(ItemRecipe recipe)
        {
            OnAddRecipe(recipe);
            OnRecipeAdded?.Invoke(recipe);
        }

        private void OnRemoved(ItemRecipe recipe)
        {
            OnRemoveRecipe(recipe);
            OnRecipeRemoved?.Invoke(recipe);
        }

        protected virtual void OnAddRecipe(ItemRecipe _) { }
        protected virtual void OnRemoveRecipe(ItemRecipe _) { }
    }
}