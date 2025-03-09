using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Modules.SaveLoad;
using Unity.Plastic.Newtonsoft.Json;

namespace Tavern.Infrastructure
{
    public abstract class InventorySerializer<T> : IGameSerializer where T : Item
    {
        private readonly IInventory<T> _inventory;
        private readonly ItemsCatalog _catalog;
        private readonly string _name;

        protected InventorySerializer(IInventory<T> inventory, ItemsCatalog catalog, string name)
        {
            _inventory = inventory;
            _catalog = catalog;
            _name = name;
        }

        public void Serialize(IDictionary<string, string> saveState)
        {
            var items = new Dictionary<string, int>();
            foreach (T item in _inventory.Items)
            {
                string key = item.ItemName;
                var value = 1;
                if (item.TryGet(out ComponentStackable stackable))
                {
                    value = stackable.Value;
                }
                
                items.Add(key, value);
            }
            
            saveState[_name] = JsonConvert.SerializeObject(items);
        }

        public void Deserialize(IDictionary<string, string> loadState)
        {
            if (!loadState.TryGetValue(_name, out string json)) return;

            var data = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
            if (data == null) return;

            _inventory.Clear();
            foreach ((string key, int value) in data)
            {
                if (!_catalog.TryGetItem(key, out ItemConfig itemConfig)) continue;
                
                if (itemConfig.Create() is not T item) continue;

                if (item.TryGet(out ComponentStackable stackable))
                {
                    stackable.Value = value;
                }
                
                _inventory.AddItem(item);
            }
        }
    }
}