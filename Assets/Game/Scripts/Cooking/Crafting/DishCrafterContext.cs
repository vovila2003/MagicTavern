using Modules.GameCycle;
using Modules.Inventories;
using Sirenix.OdinInspector;
using Tavern.Looting;
using Tavern.Storages;
using UnityEngine;
using VContainer;

namespace Tavern.Cooking
{
    public class DishCrafterContext : MonoBehaviour
    {
        private DishCrafter _dishCrafter;
        
        [ShowInInspector, ReadOnly]
        private float TimerCurrentTime => _dishCrafter?.TimerCurrentTime ?? -1;
        
        [Inject]
        private void Construct(
            IInventory<DishItem> dishInventory, 
            IInventory<LootItem> lootInventory,
            IProductsStorage productsStorage,
            IInventory<KitchenItem> kitchenInventory,
            GameCycle gameCycle)
        {
            _dishCrafter = new DishCrafter(dishInventory, lootInventory, productsStorage, kitchenInventory);
            gameCycle.AddListener(_dishCrafter);
        }

        private void OnEnable() => _dishCrafter.OnCrafted += OnCrafted;

        private void OnDisable() => _dishCrafter.OnCrafted -= OnCrafted;

        [Button]
        public void Craft(DishRecipe recipe)
        {
            if (_dishCrafter.InProgress)
            {
                Debug.Log("Crafting is in progress. Wait for finish!");    
                return;
            }
            
            if (!_dishCrafter.CanCraft(recipe))
            {
                Debug.Log($"Can't craft dish of name {recipe.ResultItem.Item.ItemName}");    
                return;
            } 
            
            _dishCrafter.Craft(recipe);
        }

        private void OnCrafted(DishItem dish)
        {
            Debug.Log($"Dish with name {dish.ItemName} is crafted!");
        }
    }
}