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
        public event Action<PlantType, int> OnPlantStorageAdded;
        public event Action<PlantType, int> OnPlantStorageChanged;
        public event Action<PlantType, int> OnPlantStorageValueSpent;
        public event Action<PlantType> OnPlantStorageFull;
        public event Action<PlantType> OnPlantStorageEmpty;

        public PlantType PlantType => Type;
        
        [ShowInInspector, ReadOnly]
        public int CurrentValue => Value;

        [SerializeField] 
        private PlantType Type;

        [SerializeField]
        private LimitType Limit = LimitType.Unlimited;

        [SerializeField]
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
        
        private void OnStorageValueAdded(int value) => OnPlantStorageAdded?.Invoke(Type, value);

        private void OnStorageValueChanged(int value) => OnPlantStorageChanged?.Invoke(Type, value);

        private void OnStorageValueSpent(int value) => OnPlantStorageValueSpent?.Invoke(Type, value);

        private void OnStorageFull() => OnPlantStorageFull?.Invoke(Type);

        private void OnStorageEmpty() => OnPlantStorageEmpty?.Invoke(Type);
    }
}