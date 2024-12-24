using Modules.GameCycle.Interfaces;
using Modules.Gardening;
using Sirenix.OdinInspector;
using Tavern.Gardening;
using Tavern.Gardening.Fertilizer;
using Tavern.Gardening.Medicine;
using Tavern.Storages;
using UnityEngine;
using VContainer;

namespace Tavern.Components
{
    public class SeederComponent :
        MonoBehaviour,
        IInitGameListener,
        IStartGameListener,
        IFinishGameListener,
        IPauseGameListener,
        IResumeGameListener
    {
        private ISeedsStorage _seedsStorage;
        private IWaterStorage _waterStorage;
        private MedicineInventoryContext _medicineInventoryContext;
        private FertilizerInventoryContext _fertilizerInventoryContext;
        private bool _isEnable;

        [Inject]
        private void Construct(ISeedsStorage seedsStorage, 
            IWaterStorage waterStorage, 
            MedicineInventoryContext medicineConsumer,
            FertilizerInventoryContext fertilizerConsumer)
        {
            _seedsStorage = seedsStorage;
            _waterStorage = waterStorage;
            _medicineInventoryContext = medicineConsumer;
            _fertilizerInventoryContext = fertilizerConsumer;
        }

        [Button]
        public void Seed(Pot pot, PlantConfig plant)
        {
            const int count = 1;
            
            if (!CanSeed(pot, plant, count, out PlantStorage storage)) return;

            bool result = pot.Seed(plant);
            if (!result) return;
            
            storage.Spend(count);
        }

        [Button]
        public void Fertilize(Pot pot, FertilizerConfig fertilizer)
        {
            if (!CanFertilize(pot, fertilizer)) return;

            if (pot.IsFertilized) return;

            _fertilizerInventoryContext.Consume(fertilizer.Item, pot);
        }

        [Button]
        public void Watering(Pot pot)
        {
            const int count = 1;
            
            if (!CanWatering(pot, count)) return;

            pot.Watering();
            _waterStorage.SpendWater(count);
        }

        [Button]
        public void Heal(Pot pot, MedicineConfig medicine)
        {
            if (!CanHeal(pot, medicine)) return;

            _medicineInventoryContext.Consume(medicine.Item, pot);
        }

        [Button]
        public void Gather(Pot pot)
        {
            if (!_isEnable) return;
            
            if (pot is null)
            {
                Debug.LogWarning("Seedbed is null");
                return;
            }
            
            pot.Gather();
        }

        void IStartGameListener.OnStart() => _isEnable = true;

        void IFinishGameListener.OnFinish() => _isEnable = false;

        void IPauseGameListener.OnPause() => _isEnable = false;

        void IResumeGameListener.OnResume() => _isEnable = true;

        void IInitGameListener.OnInit() => _isEnable = false;

        private bool CanSeed(Pot pot, PlantConfig plant, int count, out PlantStorage storage)
        {
            storage = null;
            if (!_isEnable) return false;

            if (pot is null)
            {
                Debug.LogWarning("Seedbed is null");
                return false;
            }

            if (plant is null)
            {
                Debug.LogWarning("Plant is null");
                return false;
            }

            if (!_seedsStorage.TryGetStorage(plant.Plant, out storage))
            {
                Debug.Log("Seed storage of type {type} is not found!");
                return false;
            }

            if (storage.CanSpend(count)) return true;
            
            Debug.Log("Not enough seeds of type {type} in storage!");
            return false;
        }

        private bool CanFertilize(Pot pot, FertilizerConfig fertilizer)
        {
            if (!_isEnable) return false;
            
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
            if (!_isEnable) return false;

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
            if (!_isEnable) return false;
            
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