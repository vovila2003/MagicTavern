using Modules.Items;

namespace Tavern.Shopping
{
    public interface IBuyer
    {
        bool CanBuy(int price);
        void SpendMoney(int price);
        void EarnMoney(int price);
        bool TakeItem(ItemConfig itemConfig);
        bool TakeItem(Item itemConfig);
    }
}