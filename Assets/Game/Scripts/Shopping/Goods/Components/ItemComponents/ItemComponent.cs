using System;
using Modules.Items;
using Modules.Shopping;
using UnityEngine;

namespace Tavern.Goods
{
    [Serializable]
    public class ItemComponent<T> : IGoodComponent where T : Item
    {
        [SerializeField]
        private ItemConfig<T> ItemConfig;

        private GoodsMetadata _metadata;
        private bool _initialized;

        public string Name => ItemConfig.Item.ItemName;
        public GoodsMetadata GoodsMetadata => GetMetadata();
        public ItemConfig<T> Config => ItemConfig;

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