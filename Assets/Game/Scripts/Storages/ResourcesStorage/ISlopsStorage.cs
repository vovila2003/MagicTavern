namespace Tavern.Storages
{
    public interface ISlopsStorage
    {
        void Add(float value);
        void Spend(float value);
        void ResetStorage();
        float Value { get; }
    }
}