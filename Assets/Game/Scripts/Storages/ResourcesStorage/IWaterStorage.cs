namespace Tavern.Storages
{
    public interface IWaterStorage
    {
        void AddWater(int value);
        void SpendWater(int value);
        int Water { get; }
    }
}