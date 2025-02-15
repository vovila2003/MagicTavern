using System;
using System.Collections.Generic;
using Modules.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Shopping
{
    [CreateAssetMenu(
        fileName = "SellerConfig",
        menuName = "Settings/Shopping/Seller Config")]
    public class SellerConfig : NamedConfig 
    {
        [field: SerializeField]
        public int StartMoney { get; private set; }
        
        [field: SerializeField]
        public int WeeklyMoneyBonus { get; private set; }
        
        [field: Space]
        [field: SerializeField]
        public Metadata Metadata { get; private set; }

        [Space]
        [SerializeReference] 
        public List<IComponentGroup> Filter;

        [field: SerializeField]
        public ItemConfig[] StartItems { get; private set; }

        [field: SerializeField]
        public ItemConfig[] Assortment { get; private set; }

        [field: SerializeField]
        public Preference[] Preferences { get; private set; }

        [field: SerializeField]
        public int[] Discounts { get; private set; } = new int[7];

        public Seller Create()
        {
            return new Seller(this);
        }

        private void Awake()
        {
            Filter ??= new List<IComponentGroup>();
        }

        [Button]
        private void Validate()
        {
            CheckForSellable(StartItems, "StartItems");
            CheckForSellable(Assortment, "Assortment");
            
            if (Filter.Count == 0)
            {
                Debug.Log($"Seller {Name}: filter is empty. Fill filter");
                return;
            }
            
            CheckByFilter(StartItems, "StartItems");
            CheckByFilter(Assortment, "Assortment");
        }

        private void CheckForSellable(ItemConfig[] collection, string collectionName)
        {
            foreach (ItemConfig itemConfig in collection)
            {
                if (itemConfig.Flags.HasFlag(ItemFlags.Sellable)) continue;

                Debug.LogError($"Seller {Name}: item {itemConfig.Name} from {collectionName} is not Sellable");
            }
        }

        private void CheckByFilter(ItemConfig[] collection, string collectionName)
        {
            foreach (ItemConfig itemConfig in collection)
            {
                if (!itemConfig.TryGet(out IComponentGroup componentGroup))
                {
                    Debug.LogError($"Seller {Name} : {itemConfig.Name} " +
                                   $"from {collectionName} items does not have group component");
                    continue;
                }
                
                Type type = componentGroup.GetType();
                var found = false;
                foreach (IComponentGroup filterGroup in Filter)
                {
                    Type filterType = filterGroup.GetType();
                    if (type != filterType && !type.IsSubclassOf(filterType)) continue;
                    
                    found = true;
                    break;
                }

                if (!found)
                {
                    Debug.LogError($"Seller {Name} : {itemConfig.Name} from {collectionName} does not match the filter");
                }
            }
        }
    }
}