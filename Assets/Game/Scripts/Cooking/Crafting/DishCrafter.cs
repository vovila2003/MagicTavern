using System;
using JetBrains.Annotations;
using Modules.Crafting;
using Modules.GameCycle.Interfaces;
using Modules.Gardening;
using Modules.Inventories;
using Tavern.Looting;
using Tavern.Storages;
using UnityEngine;

namespace Tavern.Cooking
{
    [UsedImplicitly]
    public class DishCrafter : 
        ItemCrafter<DishItem>,
        IExitGameListener,
        IFinishGameListener,
        IPauseGameListener,
        IResumeGameListener,
        IUpdateListener
    {
        private readonly IInventory<LootItem> _lootInventory;
        private readonly IProductsStorage _productsStorage;
        private readonly IInventory<KitchenItem> _kitchenInventory;
        
        public float TimerCurrentTime => Timer?.CurrentTime ?? -1;
        public bool InProgress => Timer.IsPlaying();

        public DishCrafter(
            IInventory<DishItem> dishInventory, 
            IInventory<LootItem> lootInventory,
            IProductsStorage productsStorage,
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
                return CheckProducts(dishRecipe) &&
                       CheckLoots(dishRecipe) &&
                       CheckKitchenItems(dishRecipe);
            }

            Debug.Log($"Recipe's type is not {nameof(DishRecipe)}!");
            return false;
        }

        private bool CheckProducts(DishRecipe dishRecipe)
        {
            foreach (ProductIngredient productIngredient in dishRecipe.Products)
            {
                int requiredAmount = productIngredient.ProductAmount;
                PlantType plantType = productIngredient.Type;
                if (!_productsStorage.TryGetStorage(plantType, out PlantStorage storage))
                {
                    Debug.Log($"Product storage of type {plantType} not found!");
                    return false;
                }

                if (storage.CurrentValue >= requiredAmount) continue;
                
                Debug.Log($"There is not enough product of type {plantType}! Required amount: {requiredAmount}. " +
                          $"Current amount: {storage.CurrentValue}");
                return false;
            }

            return true;
        }

        private bool CheckLoots(DishRecipe dishRecipe)
        {
            foreach (LootIngredient lootIngredient in dishRecipe.Loots)
            {
                int requiredAmount = lootIngredient.LootAmount;
                string lootName = lootIngredient.Loot.Item.ItemName;
                int itemCount = _lootInventory.GetItemCount(lootName);
                if (itemCount >= requiredAmount) continue;    

                Debug.Log($"There is not enough {lootName}! Required amount: {requiredAmount}. " +
                          $"Current amount: {itemCount}");
                return false;
            }

            return true;
        }

        private bool CheckKitchenItems(DishRecipe dishRecipe)
        {
            foreach (KitchenItemConfig kitchenItemConfig in dishRecipe.KitchenItems)
            {
                string kitchenItemName = kitchenItemConfig.Item.ItemName;
                int itemCount = _kitchenInventory.GetItemCount(kitchenItemName);
                if (itemCount > 0) continue;    

                Debug.Log($"There is no kitchen item {kitchenItemName}!");
                return false;
            }

            return true;
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
            foreach (ProductIngredient productIngredient in dishRecipe.Products)
            {
                int requiredAmount = productIngredient.ProductAmount;
                PlantType plantType = productIngredient.Type;
                if (!_productsStorage.TryGetStorage(plantType, out PlantStorage storage))
                {
                    throw new ArgumentException($"Product storage of type {plantType} not found!");
                }

                storage.Spend(requiredAmount);
            }
        }

        private void RemoveLoots(DishRecipe dishRecipe)
        {
            foreach (LootIngredient lootIngredient in dishRecipe.Loots)
            {
                _lootInventory.RemoveItems(lootIngredient.Loot.Item.ItemName, lootIngredient.LootAmount);    
            }
        }

        void IExitGameListener.OnExit() => Timer.Stop();

        void IFinishGameListener.OnFinish() => Timer.Stop();

        void IPauseGameListener.OnPause() => Timer.Pause();

        void IResumeGameListener.OnResume() => Timer.Resume();

        void IUpdateListener.OnUpdate(float deltaTime) => Tick(deltaTime);
    }
}