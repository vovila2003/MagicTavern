using System;
using System.Linq;
using JetBrains.Annotations;
using Modules.Crafting;
using Modules.GameCycle.Interfaces;
using Modules.Inventories;
using Modules.Items;
using Tavern.Gardening;
using Tavern.Looting;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.Cooking
{
    [UsedImplicitly]
    public class DishCrafter : 
        ItemCrafter<DishItem>,
        IExitGameListener,
        IFinishGameListener,
        IPauseGameListener,
        IResumeGameListener,
        ITickable
    {
        private readonly IInventory<LootItem> _lootInventory;
        private readonly IInventory<ProductItem> _productsStorage;
        private readonly IInventory<KitchenItem> _kitchenInventory;
        
        public float TimerCurrentTime => Timer.CurrentTime;
        public bool InProgress => Timer.IsPlaying();

        public DishCrafter(
            IInventory<DishItem> dishInventory, 
            IInventory<LootItem> lootInventory,
            IInventory<ProductItem> productsStorage,
            IInventory<KitchenItem> kitchenInventory) 
            : base(dishInventory)
        {
            _lootInventory = lootInventory;
            _productsStorage = productsStorage;
            _kitchenInventory = kitchenInventory;
        }

        public override bool CanCraft(ItemRecipe<DishItem> recipe)
        {
            if (recipe is DishRecipe dishRecipe)
            {
                return CheckProducts(dishRecipe.Products) &&
                       CheckLoots(dishRecipe.Loots) &&
                       CheckKitchens(dishRecipe.KitchenItems);
            }

            Debug.Log($"Recipe's type is not {nameof(DishRecipe)}!");
            return false;
        }
        
        private bool CheckProducts(ProductItemConfig[] configs) => 
            configs.All(config => CheckItem(_productsStorage, config));

        private bool CheckLoots(LootItemConfig[] configs) => 
            configs.All(config => CheckItem(_lootInventory, config));

        private bool CheckKitchens(KitchenItemConfig[] configs) => 
            configs.All(config => CheckItem(_kitchenInventory, config));

        private static bool CheckItem<T>(IInventory<T> storage, ItemConfig<T> config) where T : Item
        {
            string itemName = config.Item.ItemName;
            int itemCount = storage.GetItemCount(itemName);
            if (itemCount > 0) return true;    
                
            Debug.Log($"There is not enough {itemName}!");
                
            return false;
        }

        protected override void RemoveIngredientsFromInventories(ItemRecipe<DishItem> recipe)
        {
            if (recipe is not DishRecipe dishRecipe)
            {
                throw new ArgumentException($"Recipe's type is not {nameof(DishRecipe)}!");
            }

            RemoveProducts(dishRecipe);
            RemoveLoots(dishRecipe);
        }

        private void RemoveProducts(DishRecipe dishRecipe)
        {
            foreach (ProductItemConfig productIngredient in dishRecipe.Products)
            {
                _productsStorage.RemoveItem(productIngredient.Item.ItemName);
            }
        }

        private void RemoveLoots(DishRecipe dishRecipe)
        {
            foreach (LootItemConfig lootIngredient in dishRecipe.Loots)
            {
                _lootInventory.RemoveItem(lootIngredient.Item.ItemName);    
            }
        }

        void IExitGameListener.OnExit() => Timer.Stop();

        void IFinishGameListener.OnFinish() => Timer.Stop();

        void IPauseGameListener.OnPause() => Timer.Pause();

        void IResumeGameListener.OnResume() => Timer.Resume();

        void ITickable.Tick() => Tick(Time.deltaTime);
    }
}