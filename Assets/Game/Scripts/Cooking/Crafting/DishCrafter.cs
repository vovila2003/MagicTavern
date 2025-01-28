using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Modules.Inventories;
using Tavern.Settings;
using Tavern.Storages;

namespace Tavern.Cooking
{
    [UsedImplicitly]
    public class DishCrafter : IInitGameListener, IExitGameListener
    {
        public event Action<bool> OnStateChanged;
        public event Action<DishRecipe, DishItem> OnDishCrafted;
        public event Action<DishRecipe> OnSlopCrafted;
        
        private readonly IInventory<DishItem> _dishInventory;
        private readonly ISlopsStorage _slopsStorage;
        private readonly RecipeMatcher _matcher;
        private readonly ActiveDishRecipe _activeRecipe;
        private readonly EffectsCatalog _effectsCatalog;

        public DishCrafter(
            IInventory<DishItem> dishInventory,
            ISlopsStorage slopsStorage,
            RecipeMatcher matcher, 
            ActiveDishRecipe activeRecipe,
            CookingSettings settings)
        {
            _dishInventory = dishInventory;
            _slopsStorage = slopsStorage;
            _matcher = matcher;
            _activeRecipe = activeRecipe;
            _effectsCatalog = settings.Effects;
        }

        public void CraftDish(bool isExtra)
        {
            DishRecipe recipe = _activeRecipe.Recipe;
            if (recipe.ResultItem.GetItem().Clone() is not DishItem result) return;
            
            result.IsExtra = isExtra;

            if (isExtra)
            {
                ProcessExtra(result);
            }
            
            _activeRecipe.SpendIngredients();
            _dishInventory.AddItem(result);
            
            OnDishCrafted?.Invoke(recipe, result);
        }

        private void ProcessExtra(DishItem result)
        {
            List<ComponentEffect> existed = result.GetAll<ComponentEffect>();
            if (!_effectsCatalog.TryGetRandomEffectExpect(existed, out EffectConfig newEffect)) return;
            
            result.Components.Add(new ComponentEffect(newEffect));
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