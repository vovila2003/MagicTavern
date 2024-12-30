using System;
using Modules.Shopping;
using UnityEngine;

namespace Tavern.Goods
{
    [Serializable]
    public class WaterComponent : IGoodComponent
    {
        [SerializeField]
        private GoodsMetadata Metadata;
        
        public string Name => "Water";
        public GoodsMetadata GoodsMetadata => Metadata;
    }
}