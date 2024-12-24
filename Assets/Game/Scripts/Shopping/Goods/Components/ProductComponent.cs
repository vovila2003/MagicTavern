using System;
using Modules.Gardening;
using Modules.Shopping;
using UnityEngine;

namespace Tavern.Goods
{
    [Serializable]
    public class ProductComponent : IGoodComponent
    {
        [SerializeField]
        private PlantConfig PlantConfig;

        [SerializeField] 
        private int Amount;
        
        [SerializeField]
        private GoodsMetadata Metadata;
        
        public string Name => PlantConfig.Name;
        public int Count => Amount;
        public GoodsMetadata GoodsMetadata => Metadata;
        public PlantConfig Config => PlantConfig;
    }
}