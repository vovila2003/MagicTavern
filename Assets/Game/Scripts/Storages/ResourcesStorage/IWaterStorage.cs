namespace Tavern.Storages
{
    public interface IWaterStorage
    {
        void Add(float value);
        void Spend(float value);
        float Value { get; }
    }
}