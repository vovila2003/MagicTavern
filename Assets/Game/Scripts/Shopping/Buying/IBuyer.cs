using Modules.Items;

namespace Tavern.Shopping.Buying
{
    public interface IBuyer
    {
        bool CanBuy(int price);
        bool GiveMoney(int price);
        void TakeMoney(int price);
        bool TakeItem(ItemConfig itemConfig);
    }
}