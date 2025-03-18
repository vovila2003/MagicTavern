using System;

namespace Modules.Storages
{
    public class ResourceStorage : StorageInt
    {
        public event Action<int> OnResourceStorageAdded;

        public event Action<int> OnResourceStorageChanged;

        public event Action<int> OnResourceStorageValueSpent;

        public event Action OnResourceStorageFull;

        public event Action OnResourceStorageEmpty;

        public ResourceStorage(int value = 0, LimitType limitType = LimitType.Unlimited, int limitValue = 0) 
            : base(value, limitType, limitValue)
        {
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