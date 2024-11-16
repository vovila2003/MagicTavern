using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Cooking
{
    [CreateAssetMenu(fileName = "KitchenItemsCatalog", menuName = "Settings/Cooking/KitchenItems Catalog", order = 0)]
    public class KitchenItemsCatalog : ScriptableObject
    {
        [SerializeField] 
        private KitchenItemConfig[] Items;
        
        private readonly Dictionary<KitchenItemType, KitchenItemConfig> _itemsDict = new();

        public bool TryGetKitchenItem(KitchenItemType plantType, out KitchenItemConfig kitchenItemConfig) => 
            _itemsDict.TryGetValue(plantType, out kitchenItemConfig);

        private void OnValidate()
        {
            var collection = new Dictionary<KitchenItemType, bool>();
            foreach (KitchenItemConfig settings in Items)
            {
                KitchenItemType kitchenItemType = settings.KitchenItem.Type;
                _itemsDict[kitchenItemType] = settings;
                if (collection.TryAdd(kitchenItemType, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate kitchen item of type {kitchenItemType} in catalog");
            }            
        }
    }
}