using System;
using UnityEngine;

namespace Tavern.Goods
{
    [Serializable]
    public class Goods
    {
        [SerializeReference] 
        private IGoodComponent Component;
        
        public string Name => Component.Name;
        
        public IGoodComponent GoodsComponent => Component;
        
        public GoodsMetadata GoodsMetadata => Component.GoodsMetadata;
    }
}