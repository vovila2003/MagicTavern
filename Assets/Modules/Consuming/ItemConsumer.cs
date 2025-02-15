using System;
using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;

namespace Modules.Consuming
{
    public class ItemConsumer<T> where T : Item
    {
        public event Action<T> OnItemConsumed;

        private readonly List<IInventoryItemConsumeHandler> _handlers = new();

        private readonly IInventory<T> _inventory;

        public ItemConsumer(IInventory<T> inventory)
        {
            _inventory = inventory;
        }

        public void AddHandler(IInventoryItemConsumeHandler handler) => _handlers.Add(handler);

        public void RemoveHandler(IInventoryItemConsumeHandler handler) => _handlers.Remove(handler);

        public bool CanConsumeItem(T item) => 
            item.ItemFlags.HasFlag(ItemFlags.Consumable) && _inventory.IsItemExists(item);

        public bool CanConsumeItem(string name) =>
            _inventory.FindItem(name, out T item) && item.ItemFlags.HasFlag(ItemFlags.Consumable);

        public void ConsumeItem(T item, object target)
        {
            if (!CanConsumeItem(item))
            {
                throw new Exception($"Can not consume item {item.ItemName}");
            }
            
            _inventory.RemoveItem(item);
            
            OnConsumed(item, target);
        }

        public void ConsumeItem(string name, object target)
        {
            if (!CanConsumeItem(name))
            {
                return;
            }

            if (_inventory.RemoveItem(name) is T removedItem)
            {
                OnConsumed(removedItem, target);
            }
        }

        private void OnConsumed(T item, object target)
        {
            foreach (IInventoryItemConsumeHandler handler in _handlers)
            {
                handler.SetTarget(target);
                handler.OnConsume(item);
            }

            OnItemConsumed?.Invoke(item);
        }
    }
}