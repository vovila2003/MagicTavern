using System;
using Modules.Crafting;
using Modules.GameCycle.Interfaces;
using Modules.Inventories;
using Tavern.Common;
using Tavern.Looting;
using Tavern.Storages;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.Gardening.Medicine
{
    public class MedicineCrafter :
        ItemCrafter<MedicineItem>,
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

        public MedicineCrafter(
            IInventory<MedicineItem> medicineInventory, 
            IInventory<LootItem> lootInventory,
            ISlopsStorage slopsStorage) 
            : base(medicineInventory)
        {
            _lootInventory = lootInventory;
            _slopsStorage = slopsStorage;
        }

        public override bool CanCraft(ItemRecipe<MedicineItem> recipe)
        {
            if (recipe is MedicineRecipe fertilizerRecipe)
            {
                return CheckSlops(fertilizerRecipe) &&
                       CheckLoots(fertilizerRecipe);
            }

            Debug.Log($"Recipe's type is not {nameof(MedicineRecipe)}!");
            
            return false;
        }

        private bool CheckSlops(MedicineRecipe fertilizerRecipe) => 
            _slopsStorage.Slops >= fertilizerRecipe.Slops;

        private bool CheckLoots(MedicineRecipe fertilizerRecipe)
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

        protected override void RemoveIngredientsFromInventories(ItemRecipe<MedicineItem> recipe)
        {
            if (recipe is not MedicineRecipe fertilizerRecipe)
            {
                throw new ArgumentException($"Recipe's type is not {nameof(MedicineRecipe)}!");
            }

            SpendSlops(fertilizerRecipe);
            RemoveLoots(fertilizerRecipe);
        }

        private void SpendSlops(MedicineRecipe fertilizerRecipe)
        {
            _slopsStorage.SpendSlops(fertilizerRecipe.Slops);
        }

        private void RemoveLoots(MedicineRecipe fertilizerRecipe)
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