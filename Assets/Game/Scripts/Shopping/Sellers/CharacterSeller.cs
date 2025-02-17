using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Modules.Inventories;
using Modules.Items;
using Modules.Shopping;
using Sirenix.OdinInspector;
using Tavern.Storages;
using Tavern.Storages.CurrencyStorages;

namespace Tavern.Shopping
{
    [UsedImplicitly, Serializable]
    public class CharacterSeller : IInitGameListener, IExitGameListener
    {
        private readonly InventoryFacade _inventoryFacade;
        private readonly IMoneyStorage _moneyStorage;

        [ShowInInspector, ReadOnly]
        private readonly Dictionary<Item, IInventoryBase> _sellableItems = new();

        public CharacterSeller(InventoryFacade inventoryFacade, IMoneyStorage moneyStorage)
        {
            _inventoryFacade = inventoryFacade;
            _moneyStorage = moneyStorage;
        }

        void IInitGameListener.OnInit()
        {
            foreach (IInventoryBase inventory in _inventoryFacade.Inventories)
            {
                inventory.OnItemAdded += OnItemAdded;
                inventory.OnItemRemoved += OnItemRemoved;
            }
        }

        void IExitGameListener.OnExit()
        {
            foreach (IInventoryBase inventory in _inventoryFacade.Inventories)
            {
                inventory.OnItemAdded -= OnItemAdded;
                inventory.OnItemRemoved -= OnItemRemoved;
            }
        }

        private void OnItemAdded(Item item, IInventoryBase inventory)
        {
            if (item.Has<ComponentSellable>())
            {
                _sellableItems.Add(item, inventory);
            }
        }

        private void OnItemRemoved(Item item, IInventoryBase _)
        {
            if (item.Has<ComponentSellable>())
            {
                _sellableItems.Remove(item);
            }
        }

        public bool HasItem(Item item) => _sellableItems.ContainsKey(item);

        public (bool hasPrice, int price) GetItemPrice(Item item) => 
            !item.TryGet(out ComponentSellable sellable) ? (false, 0) : (true, sellable.BasePrice);

        public bool GiveItem(Item item)
        {
            if (!_sellableItems.TryGetValue(item, out IInventoryBase inventory))
                return false;

            if (!inventory.FindItem(item.ItemName, out Item _))
                return false;
            
            inventory.RemoveItem(item);
            
            return true;
        }

        public void TakeItem(Item item)
        {
            IInventoryBase inventoryBase = _inventoryFacade.GetInventory(item.TypeName);
            inventoryBase.AddItem(item);
        }

        public void TakeMoney(int price) => _moneyStorage.EarnMoney(price);
    }
}