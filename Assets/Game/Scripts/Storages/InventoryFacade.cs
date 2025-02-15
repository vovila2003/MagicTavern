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
        private readonly Dictionary<Type, IInventoryBase> _inventoryDictionary = new();

        public InventoryFacade(IReadOnlyList<IInventoryBase> inventories)
        {
            foreach (IInventoryBase inventory in inventories)
            {
                Type inventoryType = inventory.GetType();
                Type baseType = inventoryType.BaseType;
                if (baseType is null) continue;
                Type[] genericTypes = baseType.GenericTypeArguments;
                if (genericTypes.Length == 0) continue;
                _inventoryDictionary.Add(genericTypes[0], inventory);
            }
        }

        public IInventoryBase GetInventory(Type type)
        {
            return _inventoryDictionary[type];
        }
    }
}