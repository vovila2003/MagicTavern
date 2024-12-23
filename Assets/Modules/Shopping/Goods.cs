using System;
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
        
        public GoodsMetadata GoodsMetadata => Component.GoodsMetadata;
        
        public int GoodsPrice => Price;
    }
}