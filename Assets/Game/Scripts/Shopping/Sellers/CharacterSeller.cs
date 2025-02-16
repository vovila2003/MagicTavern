using System.Collections.Generic;
using Modules.GameCycle.Interfaces;
using Modules.Inventories;
using Modules.Items;
using Modules.Shopping;
using Tavern.Storages;

namespace Tavern.Shopping
{
    public class CharacterSeller : IInitGameListener, IExitGameListener
    {
        private readonly InventoryFacade _inventoryFacade;

        private HashSet<Item> _sellableItems = new();

        public CharacterSeller(InventoryFacade inventoryFacade)
        {
            _inventoryFacade = inventoryFacade;
        }

        void IInitGameListener.OnInit()
        {
            foreach (IInventoryBase inventory in _inventoryFacade.Inventories)
            {
                //inventory.OnItemAdded += OnItemAdded;
            }
        }

        void IExitGameListener.OnExit()
        {
            foreach (IInventoryBase inventory in _inventoryFacade.Inventories)
            {
                //inventory.OnItemAdded -= OnItemAdded;
            }
        }

        private void OnItemAdded(Item item)
        {
            if (item.Has<ComponentSellable>())
            {
                _sellableItems.Add(item);
            }
        }
    }
}