namespace Modules.Storages
{
    public class StorageInt : StorageBase<int>
    {
        protected StorageInt(int value = 0, LimitType limitType = LimitType.Unlimited, int limitValue = 0) 
            : base(value, limitType, limitValue)
        {
        }

        protected override int AddValues(int value1, int value2)
        {
            return value1 + value2;
        }

        protected override int SubtractValues(int value1, int value2)
        {
            return value1 - value2;
        }

        protected override bool IsGreaterOrEqual(int value1, int value2)
        {
            return value1 >= value2;
        }

        protected override bool IsGreater(int value1, int value2)
        {
            return value1 > value2;
        }

        protected override bool IsLessZero(int value)
        {
            return value < 0;
        }

        protected override bool IsLessThreshold(int value)
        {
            return value == 0;
        }
    }
}