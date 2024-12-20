using System;
using Modules.Items;
using UnityEngine;

namespace Tavern.Goods
{
    [Serializable]
    public class ItemComponent<T> : IGoodComponent where T : Item
    {
        [SerializeField]
        private ItemConfig<T> ItemConfig;

        [SerializeField] 
        private int Amount;

        private GoodsMetadata _metadata;
        private bool _initialized;

        public string Name => ItemConfig.Item.ItemName;
        public int Count => Amount;
        public GoodsMetadata GoodsMetadata => GetMetadata();

        public ItemComponent()
        {
        }

        private GoodsMetadata GetMetadata()
        {
            if (_initialized) return _metadata;
            
            _initialized = true;
            _metadata = new GoodsMetadata
            {
                Title = ItemConfig.Item.ItemMetadata.Title,
                Description = ItemConfig.Item.ItemMetadata.Description,
                Icon = ItemConfig.Item.ItemMetadata.Icon
            };
            
            return _metadata;
        }
    }
}