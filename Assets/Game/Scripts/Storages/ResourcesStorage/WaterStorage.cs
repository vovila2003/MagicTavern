
namespace Tavern.Storages
{
    public sealed class WaterStorage : BaseResourceStorageContext, IWaterStorage
    {
        public void AddWater(int value) => Add(value);

        public void SpendWater(int value) => Spend(value);

        public int Water => Value;
    }
}