using System;
using Modules.Items;
using Modules.Shopping;
using UnityEngine;

namespace Tavern.Goods
{
    [Serializable]
    public class WaterComponent : IGoodComponent
    {
        [SerializeField]
        private ItemMetadata WaterMetadata;
        
        public string Name => "Water";
        public ItemMetadata Metadata => WaterMetadata;
    }
}