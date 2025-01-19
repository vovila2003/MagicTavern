using System;
using Modules.Pools;
using Tavern.UI.Views;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class RecipeEffectSettings
    {
        [SerializeField] 
        private RecipeEffectView RecipeEffectPrefab;
        
        [SerializeField]
        private PoolLimit PoolLimit = PoolLimit.Unlimited;

        [SerializeField] 
        private int StartPoolSize = 5;
        
        public RecipeEffectView RecipeEffect => RecipeEffectPrefab;
        public int StartPoolLength => StartPoolSize;
        public PoolLimit Limit => PoolLimit;
    }
}