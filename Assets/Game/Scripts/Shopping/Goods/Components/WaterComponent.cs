using System;
using Modules.Shopping;
using UnityEngine;

namespace Tavern.Goods
{
    [Serializable]
    public class WaterComponent : IGoodComponent
    {
        [SerializeField] 
        private int Amount;
        
        [SerializeField]
        private GoodsMetadata Metadata;
        
        public string Name => "Water";
        public int Count => Amount;
        public GoodsMetadata GoodsMetadata => Metadata;
    }
}