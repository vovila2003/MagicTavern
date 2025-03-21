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
        public event Action OnSellableItemsChanged;
        
        private readonly InventoryFacade _inventoryFacade;
        private readonly IMoneyStorage _moneyStorage;

        [field: ShowInInspector]
        [field: ReadOnly]
        public Dictionary<Item, IInventoryBase> SellableItems { get; } = new();

        public CharacterSeller(InventoryFacade inventoryFacade, IMoneyStorage moneyStorage)
        {
            _inventoryFacade = inventoryFacade;
            _moneyStorage = moneyStorage;
        }
        
        public bool HasItem(Item item) => SellableItems.ContainsKey(item);

        public (bool hasPrice, int price) GetItemPrice(Item item) => 
            !item.TryGet(out ComponentSellable sellable) ? (false, 0) : (true, sellable.BasePrice);

        public bool GiveItem(Item item, int count)
        {
            if (!SellableItems.TryGetValue(item, out IInventoryBase inventory))
                return false;

            if (!inventory.FindItem(item.ItemName, out Item _))
                return false;

            inventory.RemoveItems(item, count);
            
            return true;
        }

        public void TakeItem(Item item, int count)
        {
            IInventoryBase inventoryBase = _inventoryFacade.GetInventory(item.TypeName);
            for (var i = 0; i < count; ++i)
            {
                inventoryBase.AddItem(item);
            }
        }

        public void EarnMoney(int price) => _moneyStorage.EarnMoney(price);
        
        public int GetItemCount(Item item) => 
            !item.TryGet(out ComponentStackable componentStackable) ? 1 : componentStackable.Value;

        void IInitGameListener.OnInit()
        {
            foreach (IInventoryBase inventory in _inventoryFacade.Inventories)
            {
                inventory.OnItemAdded += OnItemAdded;
                inventory.OnItemRemoved += OnItemRemoved;
                inventory.OnItemCountChanged += OnItemCountChanged;
            }
        }

        void IExitGameListener.OnExit()
        {
            foreach (IInventoryBase inventory in _inventoryFacade.Inventories)
            {
                inventory.OnItemAdded -= OnItemAdded;
                inventory.OnItemRemoved -= OnItemRemoved;
                inventory.OnItemCountChanged -= OnItemCountChanged;
            }
        }

        private void OnItemAdded(Item item, IInventoryBase inventory)
        {
            if (!item.Has<ComponentSellable>()) return;
            
            SellableItems.Add(item, inventory);
            OnSellableItemsChanged?.Invoke();
        }

        private void OnItemRemoved(Item item, IInventoryBase _)
        {
            if (!item.Has<ComponentSellable>()) return;
            
            SellableItems.Remove(item);
            OnSellableItemsChanged?.Invoke();
        }

        private void OnItemCountChanged(Item item, int count)
        {
            OnSellableItemsChanged?.Invoke();
        }
    }
}