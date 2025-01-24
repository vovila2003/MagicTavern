using System;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;

namespace Tavern.Cooking
{
    [UsedImplicitly]
    public class DishCrafter : IInitGameListener, IExitGameListener
    {
        public event Action<bool> OnStateChanged;
        
        private readonly DishInventoryContext _dishInventory;
        private readonly RecipeMatcher _matcher;
        private readonly ActiveDishRecipe _recipe;

        public DishCrafter(
            DishInventoryContext dishInventory, 
            RecipeMatcher matcher, 
            ActiveDishRecipe recipe)
        {
            _dishInventory = dishInventory;
            _matcher = matcher;
            _recipe = recipe;
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