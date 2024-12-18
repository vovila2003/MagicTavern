using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Storages
{
    [Serializable]
    public class ResourceStorage : StorageFloat
    {
        public event Action<float> OnResourceStorageAdded;
        public event Action<float> OnResourceStorageChanged;
        public event Action<float> OnResourceStorageValueSpent;
        public event Action OnResourceStorageFull;
        public event Action OnResourceStorageEmpty;

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
        
        private void OnStorageValueAdded(float value) => OnResourceStorageAdded?.Invoke(value);

        private void OnStorageValueChanged(float value) => OnResourceStorageChanged?.Invoke(value);

        private void OnStorageValueSpent(float value) => OnResourceStorageValueSpent?.Invoke(value);

        private void OnStorageFull() => OnResourceStorageFull?.Invoke();

        private void OnStorageEmpty() => OnResourceStorageEmpty?.Invoke();
    }
}