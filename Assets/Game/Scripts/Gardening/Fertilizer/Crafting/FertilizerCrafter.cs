using System;
using JetBrains.Annotations;
using Modules.Crafting;
using Modules.GameCycle.Interfaces;
using Modules.Inventories;
using Tavern.Looting;
using Tavern.Storages;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.Gardening.Fertilizer
{
    [UsedImplicitly]
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
            _slopsStorage.Slops >= fertilizerRecipe.Slops;

        private bool CheckLoots(FertilizerRecipe fertilizerRecipe)
        {
            foreach (LootItemConfig lootItemConfig in fertilizerRecipe.Loots)
            {
                string lootName = lootItemConfig.Item.ItemName;
                int itemCount = _lootInventory.GetItemCount(lootName);
                if (itemCount > 0) continue;    

                Debug.Log($"There is not enough {lootName}!");
                
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
            _slopsStorage.SpendSlops(fertilizerRecipe.Slops);
        }

        private void RemoveLoots(FertilizerRecipe fertilizerRecipe)
        {
            foreach (LootItemConfig lootItemConfig in fertilizerRecipe.Loots)
            {
                _lootInventory.RemoveItem(lootItemConfig.Item.ItemName);    
            }
        }

        void IExitGameListener.OnExit() => Timer.Stop();

        void IFinishGameListener.OnFinish() => Timer.Stop();

        void IPauseGameListener.OnPause() => Timer.Pause();

        void IResumeGameListener.OnResume() => Timer.Resume();

        void ITickable.Tick() => Tick(Time.deltaTime);
    }
}