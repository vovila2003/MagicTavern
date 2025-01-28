using System;
using Modules.Items;
using UnityEngine;

namespace Modules.Shopping
{
    [Serializable]
    public class Goods
    {
        [SerializeReference] 
        private IGoodComponent Component;

        [SerializeField]
        private int Price;
        
        public string Name => Component.Name;
        
        public IGoodComponent GoodsComponent => Component;
        
        public ItemMetadata Metadata => Component.Metadata;
        
        public int GoodsPrice => Price;
    }
}