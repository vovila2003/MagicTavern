using Modules.Items;
using UnityEngine;

namespace Tavern.Common
{
    [CreateAssetMenu(fileName = "CommonCatalog", 
        menuName = "Settings/Game Settings/Common Items Catalog", 
        order = 0)]
    public class CommonItemsCatalog : ScriptableObject, IItemsCatalog
    {
        [SerializeField] 
        private ItemsCatalog[] Catalogs;
        
        public bool TryGetItem(string itemName, out ItemConfig itemConfig)
        {
            foreach (ItemsCatalog itemsCatalog in Catalogs)
            {
                if (!itemsCatalog.TryGetItem(itemName, out itemConfig)) continue;
                
                return true;
            }

            itemConfig = null;
            
            return false;
        }
    }
}