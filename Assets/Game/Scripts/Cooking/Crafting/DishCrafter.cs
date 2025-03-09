using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Effects;
using Tavern.ProductsAndIngredients;
using Tavern.Settings;
using Tavern.Storages;

namespace Tavern.Cooking
{
    [UsedImplicitly]
    public class DishCrafter
    {
        public event Action<DishRecipe, DishItem> OnDishCrafted;
        public event Action<List<PlantProductItem> , List<AnimalProductItem>> OnSlopCrafted;
        
        private readonly IInventory<DishItem> _dishInventory;
        private readonly ISlopsStorage _slopsStorage;
        private readonly EffectsCatalog _effectsCatalog;

        public DishCrafter(
            IInventory<DishItem> dishInventory,
            ISlopsStorage slopsStorage,
            GameSettings settings)
        {
            _dishInventory = dishInventory;
            _slopsStorage = slopsStorage;
            _effectsCatalog = settings.CookingSettings.Effects;
        }

        public void CraftDish(ActiveDishRecipe activeDishRecipe, bool isExtra)
        {
            DishRecipe recipe = activeDishRecipe.Recipe;
            if (recipe.ResultItemConfig.Create() is not DishItem result) return;
            
            if (isExtra)
            {
                ProcessExtra(result);
                ProcessEffect(result);
            }
            
            activeDishRecipe.SpendIngredients();
            _dishInventory.AddItem(result);
            
            OnDishCrafted?.Invoke(recipe, result);
        }

        private void ProcessExtra(DishItem result)
        {
            if (result.Has<ComponentDishExtra>()) return;
            
            result.AddExtraComponent(new ComponentDishExtra());
        }

        private void ProcessEffect(DishItem result)
        {
            List<ComponentEffect> existed = result.GetAll<ComponentEffect>();
            if (!_effectsCatalog.TryGetRandomEffectExpect(existed, out EffectConfig newEffect)) return;
            
            result.AddExtraComponent(new ComponentEffect(newEffect));
        }

        public void MakeSlops(ActiveDishRecipe activeDishRecipe)
        {
            (List<PlantProductItem> spentPlantProducts, List<AnimalProductItem> spentLoots) = 
                activeDishRecipe.SpendIngredients();
            
            _slopsStorage.AddOneSlop();
            
            OnSlopCrafted?.Invoke(spentPlantProducts, spentLoots);
        }
    }
}