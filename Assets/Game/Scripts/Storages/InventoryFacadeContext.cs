using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Tavern.Storages
{
    public class InventoryFacadeContext : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        private InventoryFacade _inventories;
        
        [Inject]
        private void Construct(InventoryFacade inventoryFacade)
        {
            _inventories = inventoryFacade;
        }
    }
}