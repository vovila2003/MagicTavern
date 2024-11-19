using System;
using Modules.Gardening;
using Modules.Storages;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Storages
{
    [Serializable]
    public class PlantStorage : StorageInt
    {
        public event Action<Plant, int> OnPlantStorageAdded;
        public event Action<Plant, int> OnPlantStorageChanged;
        public event Action<Plant, int> OnPlantStorageValueSpent;
        public event Action<Plant> OnPlantStorageFull;
        public event Action<Plant> OnPlantStorageEmpty;

        public Plant PlantType => Plant.Plant;
        
        [ShowInInspector, ReadOnly]
        public int CurrentValue => Value;

        [SerializeField] 
        private PlantConfig Plant;

        [SerializeField]
        private LimitType Limit = LimitType.Unlimited;

        [SerializeField, ShowIf("Limit", LimitType.Limited)]
        private int MaxValue;

        public void Init()
        {
            SetLimitType(Limit);
            SetLimitValue(MaxValue);
            OnValueAdded += OnStorageValueAdded;
            OnValueChange += OnStorageValueChanged;
            OnValueSpent += OnStorageValueSpent;
            OnFull += OnStorageFull;
            OnEmpty += OnStorageEmpty;
        }

        public void Dispose()
        {
            OnValueAdded -= OnStorageValueAdded;
            OnValueChange -= OnStorageValueChanged;
            OnValueSpent -= OnStorageValueSpent;
            OnFull -= OnStorageFull;
            OnEmpty -= OnStorageEmpty;
        }
        
        private void OnStorageValueAdded(int value) => OnPlantStorageAdded?.Invoke(PlantType, value);

        private void OnStorageValueChanged(int value) => OnPlantStorageChanged?.Invoke(PlantType, value);

        private void OnStorageValueSpent(int value) => OnPlantStorageValueSpent?.Invoke(PlantType, value);

        private void OnStorageFull() => OnPlantStorageFull?.Invoke(PlantType);

        private void OnStorageEmpty() => OnPlantStorageEmpty?.Invoke(PlantType);
    }
}