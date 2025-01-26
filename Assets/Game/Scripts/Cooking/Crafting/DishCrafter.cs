using System;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Modules.Inventories;
using Tavern.Storages;

namespace Tavern.Cooking
{
    [UsedImplicitly]
    public class DishCrafter : IInitGameListener, IExitGameListener
    {
        public event Action<bool> OnStateChanged;
        public event Action<DishRecipe> OnDishCrafted;
        
        private readonly IStackableInventory<DishItem> _dishInventory;
        private readonly ISlopsStorage _slopsStorage;
        private readonly RecipeMatcher _matcher;
        private readonly ActiveDishRecipe _recipe;

        public DishCrafter(
            IStackableInventory<DishItem> dishInventory,
            ISlopsStorage slopsStorage,
            RecipeMatcher matcher, 
            ActiveDishRecipe recipe)
        {
            _dishInventory = dishInventory;
            _slopsStorage = slopsStorage;
            _matcher = matcher;
            _recipe = recipe;
        }

        public void CraftDish()
        {
            DishRecipe recipe = _recipe.Recipe;
            var result = recipe.ResultItem.Item.Clone() as DishItem;
            _recipe.SpendIngredients();
            _dishInventory.AddItem(result);
            OnDishCrafted?.Invoke(recipe);
        }

        public void MakeSlops()
        {
            _recipe.SpendIngredients();
            _slopsStorage.AddOneSlop();
        }

        void IInitGameListener.OnInit()
        {
            _matcher.OnRecipeMatched += OnRecipeMatched;
        }

        void IExitGameListener.OnExit()
        {
            _matcher.OnRecipeMatched -= OnRecipeMatched;
        }

        private void OnRecipeMatched(bool matched)
        {
            OnStateChanged?.Invoke(matched && _recipe.CanTryCraft());
        }
    }
}