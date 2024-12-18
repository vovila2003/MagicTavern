using System;
using Modules.Crafting;
using Modules.GameCycle.Interfaces;
using Modules.Inventories;
using Tavern.Common;
using Tavern.Looting;
using Tavern.Storages;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.Gardening.Fertilizer
{
    public class FertilizerCrafter : 
        ItemCrafter<FertilizerItem>,
        IExitGameListener,
        IFinishGameListener,
        IPauseGameListener,
        IResumeGameListener,
        ITickable
    {
        private readonly IInventory<LootItem> _lootInventory;
        private readonly ISlopsStorage _slopsStorage;
        
        public float TimerCurrentTime => Timer.CurrentTime;
        public bool InProgress => Timer.IsPlaying();

        public FertilizerCrafter(
            IInventory<FertilizerItem> fertilizerInventory, 
            IInventory<LootItem> lootInventory,
            ISlopsStorage slopsStorage) 
            : base(fertilizerInventory)
        {
            _lootInventory = lootInventory;
            _slopsStorage = slopsStorage;
        }

        public override bool CanCraft(ItemRecipe<FertilizerItem> recipe)
        {
            if (recipe is FertilizerRecipe fertilizerRecipe)
            {
                return CheckSlops(fertilizerRecipe) &&
                       CheckLoots(fertilizerRecipe);
            }

            Debug.Log($"Recipe's type is not {nameof(FertilizerRecipe)}!");
            
            return false;
        }

        private bool CheckSlops(FertilizerRecipe fertilizerRecipe) => 
            _slopsStorage.Value >= fertilizerRecipe.Slops;

        private bool CheckLoots(FertilizerRecipe fertilizerRecipe)
        {
            foreach (LootIngredient lootIngredient in fertilizerRecipe.Loots)
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

        protected override void RemoveIngredientsFromInventories(ItemRecipe<FertilizerItem> recipe)
        {
            if (recipe is not FertilizerRecipe fertilizerRecipe)
            {
                throw new ArgumentException($"Recipe's type is not {nameof(FertilizerRecipe)}!");
            }

            SpendSlops(fertilizerRecipe);
            RemoveLoots(fertilizerRecipe);
        }

        private void SpendSlops(FertilizerRecipe fertilizerRecipe)
        {
            _slopsStorage.Spend(fertilizerRecipe.Slops);
        }

        private void RemoveLoots(FertilizerRecipe fertilizerRecipe)
        {
            foreach (LootIngredient lootIngredient in fertilizerRecipe.Loots)
            {
                _lootInventory.RemoveItems(lootIngredient.Loot.Item.ItemName, lootIngredient.LootAmount);    
            }
        }

        void IExitGameListener.OnExit() => Timer.Stop();

        void IFinishGameListener.OnFinish() => Timer.Stop();

        void IPauseGameListener.OnPause() => Timer.Pause();

        void IResumeGameListener.OnResume() => Timer.Resume();

        void ITickable.Tick() => Tick(Time.deltaTime);
    }
}