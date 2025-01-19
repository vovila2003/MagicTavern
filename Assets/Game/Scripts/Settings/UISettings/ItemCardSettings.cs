using System;
using Modules.Pools;
using Tavern.UI.Views;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class ItemCardSettings
    {
        [SerializeField] 
        private ItemCardView ItemCardPrefab;
        
        [SerializeField]
        private PoolLimit PoolLimit = PoolLimit.Unlimited;

        [SerializeField] 
        private int StartPoolSize = 5;
        
        public ItemCardView ItemCard => ItemCardPrefab;
        public int StartPoolLength => StartPoolSize;
        public PoolLimit Limit => PoolLimit;
    }
}