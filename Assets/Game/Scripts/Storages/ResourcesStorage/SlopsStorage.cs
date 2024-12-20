namespace Tavern.Storages
{
    public sealed class SlopsStorage : BaseResourceStorageContext, ISlopsStorage
    {
        public void AddSlops(int value) => Add(value);

        public void SpendSlops(int value) => Spend(value);

        public int Slops => Value;
    }
}