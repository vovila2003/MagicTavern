using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Modules.SaveLoad;
using Tavern.Utils;

namespace Tavern.Infrastructure
{
    public abstract class BaseInventorySerializer<T> : IGameSerializer where T : Item
    {
        private readonly IInventory<T> _inventory;
        private readonly ItemsCatalog _catalog;
        private readonly ItemSerializer _serializer;
        private readonly string _name;

        protected BaseInventorySerializer(
            IInventory<T> inventory,
            ItemsCatalog catalog,
            ItemSerializer serializer,
            string name)
        {
            _inventory = inventory;
            _catalog = catalog;
            _serializer = serializer;
            _name = name;
        }

        public void Serialize(IDictionary<string, string> saveState)
        {
            var items = new List<ItemData>();
            foreach (T item in _inventory.Items)
            {
                items.Add(_serializer.Serialize(item));
            }
            
            saveState[_name] = Serializer.SerializeObject(items);
        }

        public void Deserialize(IDictionary<string, string> loadState)
        {
            if (!loadState.TryGetValue(_name, out string json)) return;

            (List<ItemData> items, bool ok) = 
                Serializer.DeserializeObject<List<ItemData>>(json);
            if (!ok) return;

            _inventory.Clear();
            foreach (ItemData itemData in items)
            {
                Item item = _serializer.Deserialize<T>(itemData, _catalog);
                if (item == null) continue;
                
                _inventory.AddItem(item);
            }
        }
    }
}