namespace Tavern.Storages
{
    public sealed class SlopsStorage : BaseResourceStorageContext, ISlopsStorage
    {
        public void AddSlops(int value) => Add(value);
        
        public void AddOneSlop() => Add(1);

        public void SpendSlops(int value) => Spend(value);

        public int Slops => Value;
    }
}