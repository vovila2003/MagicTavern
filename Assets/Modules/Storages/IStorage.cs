using System;

namespace Modules.Storages
{
    public interface IStorage<T>
    {
        event Action<T> OnValueChange;
        event Action<T> OnValueAdded;
        event Action<T> OnValueSpent;
        event Action OnFull;
        event Action OnEmpty;
        public T Value { get; }
        public LimitType LimitType { get; }
        public T LimitValue { get; }

        void SetLimitType(LimitType limitType);
        void SetLimitValue(T value);
        bool Add(T value);
        bool CanSpend(T value);
        bool Spend(T value);
        void Reset();
    }
}