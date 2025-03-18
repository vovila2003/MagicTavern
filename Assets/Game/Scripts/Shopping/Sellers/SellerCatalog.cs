using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Shopping
{
    [CreateAssetMenu(
        fileName = "SellerCatalog",
        menuName = "Settings/Shopping/Seller Catalog")]
    public class SellerCatalog : ScriptableObject
    {
        [field: SerializeField] 
        public List<SellerConfig> Sellers { get; protected set; } = new();

        private readonly Dictionary<string, SellerConfig> _sellersDict = new();

        public bool TryGetSeller(string itemName, out SellerConfig itemConfig) => 
            _sellersDict.TryGetValue(itemName, out itemConfig);

        [Button]
        private void Validate()
        {
            OnValidate();
        }

        private void Awake()
        {
            _sellersDict.Clear();
            foreach (SellerConfig settings in Sellers)
            {
                if (settings?.Name != null)
                {
                    _sellersDict.Add(settings.Name, settings);
                }
            }
        }

        private void OnValidate()
        {
            var collection = new Dictionary<string, bool>();
            _sellersDict.Clear();
            foreach (SellerConfig settings in Sellers)
            {
                string sellerName = settings.Name;
                if (sellerName is null)
                {
                    Debug.LogWarning("Seller has empty name in sellers catalog");
                    continue;
                }
                _sellersDict.Add(settings.Name, settings);
                
                if (collection.TryAdd(sellerName, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate seller of name {sellerName} in catalog");
            }
        }
    }
}