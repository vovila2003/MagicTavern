using System;
using Modules.Pools;
using Sirenix.OdinInspector;
using Tavern.UI.Views;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class EntityCardSettings
    {
        [SerializeField] 
        private EntityCardView EntityCardPrefab;
        
        [SerializeField]
        private PoolLimit PoolLimit = PoolLimit.Unlimited;

        [SerializeField] 
        private int StartPoolSize = 5;
        
        public EntityCardView EntityCard => EntityCardPrefab;
        public int StartPoolLength => StartPoolSize;
        public PoolLimit Limit => PoolLimit;
    }
}