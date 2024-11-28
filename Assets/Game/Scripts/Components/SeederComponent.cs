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
            if (!_isEnable) return;
            
            if (pot is null)
            {
                Debug.LogWarning("Seedbed is null");
                return;
            }

            if (plant is null)
            {
                Debug.LogWarning("Plant is null");
                return;
            }
            
            if (!_seedsStorage.TryGetStorage(plant.Plant, out PlantStorage storage))
            {
                Debug.Log("Seed storage of type {type} is not found!");
                return;
            }

            const int count = 1;

            if (!storage.CanSpend(count))
            {
                Debug.Log("Not enough seeds of type {type} in storage!");
                return;
            }

            bool result = pot.Seed(plant);
            if (!result) return;
            
            storage.Spend(count);
        }

        [Button]
        public void Fertilize(Pot pot, FertilizerConfig fertilizer)
        {
            if (!_isEnable) return;
            
            if (pot is null)
            {
                Debug.LogWarning("Seedbed is null");
                return;
            }

            if (!pot.IsSeeded) return;
            
            if (fertilizer is null)
            {
                Debug.LogWarning("Fertilizer is null");
                return;
            }

            if (pot.IsFertilized) return;

            _fertilizerInventoryContext.Consume(fertilizer.Item, pot);
        }

        [Button]
        public void Watering(Pot pot)
        {
            if (!_isEnable) return;
            
            if (pot is null)
            {
                Debug.LogWarning("Seedbed is null");
                return;
            }
            
            const int count = 1;

            if (_waterStorage.Value < count)
            {
                Debug.Log("Not enough water in storage!");
                return;
            }

            pot.Watering();
            _waterStorage.Spend(count);
        }

        [Button]
        public void Heal(Pot pot, MedicineConfig medicine)
        {
            if (!_isEnable) return;
            if (pot is null)
            {
                Debug.LogWarning("Seedbed is null");
                return;
            }
            
            _medicineInventoryContext.Consume(medicine.Item, pot);            
        }

        void IStartGameListener.OnStart() => _isEnable = true;

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

        void IFinishGameListener.OnFinish() => _isEnable = false;

        void IPauseGameListener.OnPause() => _isEnable = false;

        void IResumeGameListener.OnResume() => _isEnable = true;

        void IInitGameListener.OnInit() => _isEnable = false;
    }
}