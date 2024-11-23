using Modules.GameCycle.Interfaces;
using Modules.Gardening;
using Modules.Inventories;
using Sirenix.OdinInspector;
using Tavern.Gardening;
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
        private IInventory<MedicineItem> _medicineInventory;
        private bool _isEnable;

        [Inject]
        private void Construct(ISeedsStorage seedsStorage, IWaterStorage waterStorage, 
            IInventory<MedicineItem> medicineInventory)
        {
            _seedsStorage = seedsStorage;
            _waterStorage = waterStorage;
            _medicineInventory = medicineInventory;
        }

        [Button]
        public void Seed(Seedbed seedbed, PlantConfig plant)
        {
            if (!_isEnable) return;
            
            if (seedbed is null)
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

            bool result = seedbed.Seed(plant);
            if (!result) return;
            
            storage.Spend(count);
        }
        
        [Button]
        public void Gather(Seedbed seedbed)
        {
            if (!_isEnable) return;
            
            if (seedbed is null)
            {
                Debug.LogWarning("Seedbed is null");
                return;
            }
            
            seedbed.Gather();
        }

        [Button]
        public void Watering(Seedbed seedbed)
        {
            if (!_isEnable) return;
            
            if (seedbed is null)
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

            seedbed.Watering();
            _waterStorage.Spend(count);
        }

        [Button]
        public void Heal(Seedbed seedbed, MedicineConfig medicine)
        {
            if (!_isEnable) return;
            if (seedbed is null)
            {
                Debug.LogWarning("Seedbed is null");
                return;
            }
            
            if (_medicineInventory.GetItemCount(medicine.Item.ItemName) <= 0)
            {
                Debug.Log($"Medicine of type {medicine.Item.ItemName} is not found!");
                return;
            }

            seedbed.Heal(medicine);
            _medicineInventory.RemoveItem(medicine.Item.ItemName);            
        }

        void IStartGameListener.OnStart() => _isEnable = true;

        void IFinishGameListener.OnFinish() => _isEnable = false;

        void IPauseGameListener.OnPause() => _isEnable = false;

        void IResumeGameListener.OnResume() => _isEnable = true;

        void IInitGameListener.OnInit() => _isEnable = false;
    }
}