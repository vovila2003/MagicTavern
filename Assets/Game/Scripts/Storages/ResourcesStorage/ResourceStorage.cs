using System;
using Modules.Gardening;
using Modules.Storages;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Storages
{
    [Serializable]
    public class ResourceStorage : StorageFloat
    {
        public event Action<CaringType, float> OnResourceStorageAdded;
        public event Action<CaringType, float> OnResourceStorageChanged;
        public event Action<CaringType, float> OnResourceStorageValueSpent;
        public event Action<CaringType> OnResourceStorageFull;
        public event Action<CaringType> OnResourceStorageEmpty;

        public CaringType ResourceType => Type;
        
        [ShowInInspector, ReadOnly]
        public float CurrentValue => Value;

        [SerializeField] 
        private CaringType Type;

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
        
        private void OnStorageValueAdded(float value) => OnResourceStorageAdded?.Invoke(Type, value);

        private void OnStorageValueChanged(float value) => OnResourceStorageChanged?.Invoke(Type, value);

        private void OnStorageValueSpent(float value) => OnResourceStorageValueSpent?.Invoke(Type, value);

        private void OnStorageFull() => OnResourceStorageFull?.Invoke(Type);

        private void OnStorageEmpty() => OnResourceStorageEmpty?.Invoke(Type);
    }
}