namespace Modules.Storages
{
    public class StorageFloat : StorageBase<float>
    {
        private const float Threshold = 10E-4f;
        
        public StorageFloat(float value = 0, LimitType limitType = LimitType.Unlimited, float limitValue = 0) 
            : base(value, limitType, limitValue)
        {
        }

        protected override float AddValues(float value1, float value2)
        {
            return value1 + value2;
        }

        protected override float SubtractValues(float value1, float value2)
        {
            return value1 - value2;
        }

        protected override bool IsGreaterOrEqual(float value1, float value2)
        {
            return value1 >= value2;
        }

        protected override bool IsGreater(float value1, float value2)
        {
            return value1 > value2;
        }

        protected override bool IsLessZero(float value)
        {
            return value < 0;
        }

        protected override bool IsLessThreshold(float value)
        {
            return value < Threshold;
        }
    }
}