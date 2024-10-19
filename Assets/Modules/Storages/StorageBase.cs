using System;

namespace Modules.Storages
{
    public abstract class StorageBase<T> : IStorage<T>
    {
        public event Action<T> OnValueChange;
        public event Action<T> OnValueAdded;
        public event Action<T> OnValueSpent;
        public event Action OnFull;
        public event Action OnEmpty;

        public T Value { get; private set; }
        public LimitType LimitType { get; private set; }
        public T LimitValue { get; private set; }

        protected StorageBase( T value, LimitType limitType, T limitValue)
        {
            LimitValue = limitValue;
            LimitType = limitType;
            Value = value;
        }

        public void SetLimitType(LimitType limitType)
        {
            LimitType = limitType;
        }

        public void SetLimitValue(T value)
        {
            LimitValue = value;
        }

        public void Reset()
        {
            Value = default;
            OnEmpty?.Invoke();
        }
        
        public bool Add(T value)
        {
            if (IsLessZero(value)) return false;
            
            if (LimitType == LimitType.Unlimited)
            {
                Value = AddValues(Value, value);
            }
            
            if (LimitType == LimitType.Limited)
            {
                if (IsGreaterOrEqual(Value, LimitValue))
                {
                    return false;
                }
                
                Value = AddValues(Value, value);
                if (IsGreaterOrEqual(Value, LimitValue))
                {
                    value = SubtractValues(LimitValue, Value);
                    Value = LimitValue;
                    OnFull?.Invoke();
                }
            }
            
            OnValueChange?.Invoke(Value);
            OnValueAdded?.Invoke(value);
            return true;
        }

        public bool CanSpend(T value)
        {
            if (IsLessZero(value)) return false;
            
            return IsGreaterOrEqual(Value, value);
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
        protected abstract bool IsGreaterOrEqual(T value1, T value2);
        protected abstract bool IsLessZero(T value);
        protected abstract bool IsLessThreshold(T value);
    }
}