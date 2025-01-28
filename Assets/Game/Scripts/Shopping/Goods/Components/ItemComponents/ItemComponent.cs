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

        private ItemMetadata _metadata;
        private bool _initialized;

        public virtual string Name => ItemConfig.Item.ItemName;
        public ItemMetadata Metadata => ItemConfig.Metadata;
        public ItemConfig<T> Config => ItemConfig;
    }
}