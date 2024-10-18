using System;

namespace Modules.Storages
{
    public abstract class StorageBase<T> : IStorage<T>
    {
        public event Action<T> OnValueChange;
        public event Action<T> OnValueAdded;
        public event Action<T> OnValueSpent;
        public event Action OnStorageIsFull;
        public event Action OnStorageIsEmpty;

        public T Value { get; private set; }
        public LimitType LimitType { get; }
        public T LimitValue { get; }

        protected StorageBase( T value, LimitType limitType, T limitValue)
        {
            LimitValue = limitValue;
            LimitType = limitType;
            Value = value;
        }

        public void Reset()
        {
            Value = default;
            OnStorageIsEmpty?.Invoke();
        }
        
        public bool Add(T value)
        {
            if (IsLessZero(value)) return false;
            
            T oldValue = Value;
            Value = AddValues(Value, value);

            if (LimitType == LimitType.Limited && IsGreater(Value, LimitValue))
            {
                value = SubtractValues(LimitValue, oldValue);
                Value = LimitValue;
                OnStorageIsFull?.Invoke();
            }
            
            OnValueChange?.Invoke(Value);
            OnValueAdded?.Invoke(value);
            return true;
        }

        public bool CanSpend(T value)
        {
            if (IsLessZero(value)) return false;
            
            return IsGreater(Value, value);
        }

        public bool Spend(T value)
        {
            if (!CanSpend(value)) return false;
            
            Value = SubtractValues(Value, value);
            OnValueChange?.Invoke(Value);
            OnValueSpent?.Invoke(value);
            
            if (IsLessThreshold(Value))
            {
                Reset();
            }

            return true;
        }

        protected abstract T AddValues(T value1, T value2);
        protected abstract T SubtractValues(T value1, T value2);
        protected abstract bool IsGreater(T value1, T value2);
        protected abstract bool IsLessZero(T value);
        protected abstract bool IsLessThreshold(T value);
    }
}