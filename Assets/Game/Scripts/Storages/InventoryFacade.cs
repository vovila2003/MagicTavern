using System;
using System.Collections.Generic;
using Modules.Inventories;
using Sirenix.OdinInspector;

namespace Tavern.Storages
{
    [Serializable]
    public class InventoryFacade
    {
        [ShowInInspector, ReadOnly]
        private readonly Dictionary<string, IInventoryBase> _inventoryDictionary = new();
        
        public Dictionary<string, IInventoryBase>.ValueCollection Inventories => _inventoryDictionary.Values;

        public InventoryFacade(IReadOnlyList<IInventoryBase> inventories)
        {
            foreach (IInventoryBase inventory in inventories)
            {
                Type inventoryType = inventory.GetType();
                Type baseType = inventoryType.BaseType;
                if (baseType is null) continue;
                
                Type[] genericTypes = baseType.GenericTypeArguments;
                if (genericTypes.Length == 0) continue;
                
                _inventoryDictionary.Add(genericTypes[0].Name, inventory);
            }
        }

        public IInventoryBase GetInventory(string type) => _inventoryDictionary[type];
    }
}