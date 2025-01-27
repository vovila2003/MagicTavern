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
        public event Action<DishRecipe> OnSlopCrafted;
        
        private readonly IStackableInventory<DishItem> _dishInventory;
        private readonly ISlopsStorage _slopsStorage;
        private readonly RecipeMatcher _matcher;
        private readonly ActiveDishRecipe _activeRecipe;

        public DishCrafter(
            IStackableInventory<DishItem> dishInventory,
            ISlopsStorage slopsStorage,
            RecipeMatcher matcher, 
            ActiveDishRecipe activeRecipe)
        {
            _dishInventory = dishInventory;
            _slopsStorage = slopsStorage;
            _matcher = matcher;
            _activeRecipe = activeRecipe;
        }

        public void CraftDish()
        {
            DishRecipe recipe = _activeRecipe.Recipe;
            var result = recipe.ResultItem.Item.Clone() as DishItem;
            _activeRecipe.SpendIngredients();
            _dishInventory.AddItem(result);
            
            OnDishCrafted?.Invoke(recipe);
        }

        public void MakeSlops()
        {
            DishRecipe recipe = _activeRecipe.Recipe;
            _activeRecipe.SpendIngredients();
            _slopsStorage.AddOneSlop();
            
            OnSlopCrafted?.Invoke(recipe);
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
            OnStateChanged?.Invoke(matched && _activeRecipe.CanTryCraft());
        }
    }
}