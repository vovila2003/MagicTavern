namespace Tavern.Storages
{
    public interface ISlopsStorage
    {
        void AddSlops(int value);
        void AddOneSlop();
        void SpendSlops(int value);
        int Slops { get; }
    }
}