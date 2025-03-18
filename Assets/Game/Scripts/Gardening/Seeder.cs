using Modules.Consuming;
using Modules.Inventories;
using Tavern.Gardening.Fertilizer;
using Tavern.Gardening.Medicine;
using Tavern.Storages;
using UnityEngine;
using VContainer;

namespace Tavern.Gardening
{
    public class Seeder
    {
        private IInventory<SeedItem> _seedsStorage;
        private IWaterStorage _waterStorage;
        private IConsumable _medicineInventory;
        private IConsumable _fertilizerInventory;

        [Inject]
        private void Construct(
            IInventory<SeedItem> seedsStorage, 
            IWaterStorage waterStorage, 
            MedicineInventory medicineInventory,
            FertilizerInventory fertilizerInventory)
        {
            _seedsStorage = seedsStorage;
            _waterStorage = waterStorage;
            _medicineInventory = medicineInventory;
            _fertilizerInventory = fertilizerInventory;
        }

        public bool Seed(Pot pot, SeedItemConfig seedConfig)
        {
            if (!CanSeed(pot, seedConfig)) return false;

            bool result = pot.Seed(seedConfig);
            if (!result) return false;
            
            _seedsStorage.RemoveItem(seedConfig.Name);
            
            return true;
        }

        public bool Fertilize(Pot pot, FertilizerConfig fertilizer)
        {
            if (!CanFertilize(pot, fertilizer)) return false;

            if (pot.IsFertilized) return false;

            _fertilizerInventory.Consume(fertilizer, pot);

            return true;
        }

        public bool Watering(Pot pot)
        {
            const int count = 1;
            
            if (!CanWatering(pot, count)) return false;

            pot.Watering();
            _waterStorage.SpendWater(count);

            return true;
        }

        public bool Heal(Pot pot, MedicineConfig medicine)
        {
            if (!CanHeal(pot, medicine)) return false;

            _medicineInventory.Consume(medicine, pot);

            return true;
        }

        public bool Gather(Pot pot)
        {
            if (pot is null)
            {
                Debug.LogWarning("Seedbed is null");
                return false;
            }
            
            pot.Gather();

            return true;
        }

        private bool CanSeed(Pot pot, SeedItemConfig seedConfig)
        {
            if (pot is null)
            {
                Debug.LogWarning("Seedbed is null");
                return false;
            }

            if (seedConfig is null)
            {
                Debug.LogWarning("Seed is null");
                return false;
            }

            int seedCountAvailable = _seedsStorage.GetItemCount(seedConfig.Name);
            if (seedCountAvailable >= 1) return true;
            
            Debug.Log($"Not enough seeds of type {seedConfig.Name} in storage!");
            return false;
        }

        private bool CanFertilize(Pot pot, FertilizerConfig fertilizer)
        {
            if (pot is null)
            {
                Debug.LogWarning("Seedbed is null");
                return false;
            }

            if (!pot.IsSeeded) return false;

            if (fertilizer is not null) return true;
            
            Debug.LogWarning("Fertilizer is null");
            return false;
        }

        private bool CanWatering(Pot pot, int count)
        {
            if (pot is null)
            {
                Debug.LogWarning("Seedbed is null");
                return false;
            }

            if (!(_waterStorage.Water < count)) return true;
            
            Debug.Log("Not enough water in storage!");
            return false;
        }

        private bool CanHeal(Pot pot, MedicineConfig medicine)
        {
            if (pot is null)
            {
                Debug.LogWarning("Seedbed is null");
                return false;
            }

            if (!pot.IsSick) return false;

            if (medicine is not null) return true;
            
            Debug.LogWarning("Medicine is null");
            return false;
        }
    }
}