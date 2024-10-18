using System;

namespace Modules.Storages
{
    public interface IStorage<T>
    {
        event Action<T> OnValueChange;
        event Action<T> OnValueAdded;
        event Action<T> OnValueSpent;
        event Action OnStorageIsFull;
        event Action OnStorageIsEmpty;
        public T Value { get; }
        public LimitType LimitType { get; }
        public T LimitValue { get; }
        bool Add(T value);
        bool CanSpend(T value);
        bool Spend(T value);
        void Reset();
    }
}