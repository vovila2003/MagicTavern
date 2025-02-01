using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Shopping
{
    [CreateAssetMenu(
        fileName = "GoodsCatalog",
        menuName = "Settings/Shopping/Goods Catalog")]
    public class GoodsCatalog : ScriptableObject 
    {
        [SerializeField] 
        private GoodsConfig[] GoodsConfigs;

        private readonly Dictionary<string, GoodsConfig> _goodsDict = new();
        
        public bool TryGetGoods(string goodsName, out GoodsConfig goodsConfig) => 
            _goodsDict.TryGetValue(goodsName, out goodsConfig);

        [Button]
        private void OnValidate()
        {
            var collection = new Dictionary<string, bool>();
            foreach (GoodsConfig config in GoodsConfigs)
            {
                string goodsName = config.Name;
                _goodsDict[goodsName] = config;
                if (collection.TryAdd(goodsName, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate goods of name {goodsName} in catalog");
            }            
        }
    }
}