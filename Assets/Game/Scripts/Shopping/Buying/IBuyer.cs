using Modules.Items;

namespace Tavern.Shopping
{
    public interface IBuyer
    {
        bool CanBuy(int price);
        void GiveMoney(int price);
        void TakeMoney(int price);
        bool TakeItem(ItemConfig itemConfig);
    }
}