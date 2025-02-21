using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Storages
{
    [Serializable]
    public class ResourceStorage : StorageInt
    {
        public event Action<int> OnResourceStorageAdded;

        public event Action<int> OnResourceStorageChanged;

        public event Action<int> OnResourceStorageValueSpent;

        public event Action OnResourceStorageFull;

        public event Action OnResourceStorageEmpty;

        [SerializeField]
        private LimitType Limit = LimitType.Unlimited;

        [SerializeField, ShowIf("Limit", LimitType.Limited)]
        private int MaxValue;

        [ShowInInspector, ReadOnly]
        private int ResourceValue => Value;

        public ResourceStorage(int value = 0, LimitType limitType = LimitType.Unlimited, int limitValue = 0) 
            : base(value, limitType, limitValue)
        {
        }

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
        
        private void OnStorageValueAdded(int value) => OnResourceStorageAdded?.Invoke(value);

        private void OnStorageValueChanged(int value) => OnResourceStorageChanged?.Invoke(value);

        private void OnStorageValueSpent(int value) => OnResourceStorageValueSpent?.Invoke(value);

        private void OnStorageFull() => OnResourceStorageFull?.Invoke();

        private void OnStorageEmpty() => OnResourceStorageEmpty?.Invoke();
    }
}