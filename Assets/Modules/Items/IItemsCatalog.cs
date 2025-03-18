namespace Modules.Items
{
    public interface IItemsCatalog
    {
        bool TryGetItem(string itemName, out ItemConfig itemConfig);
    }
}