using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Inventories;
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
            CookingSettings settings)
        {
            _dishInventory = dishInventory;
            _slopsStorage = slopsStorage;
            _effectsCatalog = settings.Effects;
        }

        public void CraftDish(ActiveDishRecipe activeDishRecipe, bool isExtra)
        {
            DishRecipe recipe = activeDishRecipe.Recipe;
            if (recipe.ResultItemConfig.Create() is not DishItem result) return;
            
            result.IsExtra = isExtra;
            
            if (isExtra)
            {
                ProcessExtra(result);
            }
            
            activeDishRecipe.SpendIngredients();
            _dishInventory.AddItem(result);
            
            OnDishCrafted?.Invoke(recipe, result);
        }

        private void ProcessExtra(DishItem result)
        {
            List<ComponentEffect> existed = result.GetAll<ComponentEffect>();
            if (!_effectsCatalog.TryGetRandomEffectExpect(existed, out EffectConfig newEffect)) return;
            
            result.AddComponent(new ComponentEffect(newEffect));
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