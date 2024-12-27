using Modules.Inventories;
using Modules.Items;
using Tavern.Goods;

namespace Tavern.Buying
{
    public abstract class ItemBuyCompleter<T> : IGoodsBuyCompleter where T : Item
    {
        private readonly IInventory<T> _inventory;

        protected ItemBuyCompleter(IInventory<T> inventory)
        {
            _inventory = inventory;
        }

        public void CompleteBuy(Modules.Shopping.Goods goods)
        {
            if (goods.GoodsComponent is not ItemComponent<T> itemComponent) return;

            var item = itemComponent.Config.Item.Clone() as T;
            _inventory.AddItem(item);
        }
    }
}