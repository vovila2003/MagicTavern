using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.SaveLoad;
using Tavern.Settings;
using Tavern.Shopping;
using Tavern.Utils;
using UnityEngine;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class ShopsSerializer : IGameSerializer
    {
        [Serializable]
        public class ItemConfigData
        {
            public string Name;
            public int Count;
            public int Price;
        }
        
        [Serializable]
        public class ShopData
        {
            public float[] Position;
            public float[] Rotation;
            public string ConfigName;
            public int Money;
            public int Reputation;
            public List<ItemConfigData> Items;
            public List<ItemData> CharacterItems;
        }
        
        private const string Shops = "Shops";
        
        private readonly ShopFactory _factory;
        private readonly NpcSellerSerializer _npcSellerSerializer;
        private readonly SellerCatalog _catalog;

        public ShopsSerializer(ShopFactory factory, ItemSerializer itemSerializer, GameSettings gameSettings)
        {
            _factory = factory;
            _catalog = gameSettings.ShoppingSettings.SellerCatalog;
            _npcSellerSerializer = new NpcSellerSerializer(
                itemSerializer, 
                gameSettings.SaveLoadSettings.CommonItemsCatalog);
        }

        public void Serialize(IDictionary<string, string> saveState)
        {
            var shops = new List<ShopData>(_factory.Shops.Count);
            foreach (ShopContext shopContext in _factory.Shops.Keys)
            {
                Transform transform = shopContext.transform;
                var shopData = new ShopData
                {
                    Position = transform.position.ToFloat3(),
                    Rotation = transform.rotation.ToFloat4(),
                    ConfigName = shopContext.SellerConfig.Name
                };
                _npcSellerSerializer.Serialize(shopContext.Shop.NpcSeller, shopData);
                shops.Add(shopData);
            }
    
            saveState[Shops] = Serializer.SerializeObject(shops);
        }

        public void Deserialize(IDictionary<string, string> loadState)
        {
            if (!loadState.TryGetValue(Shops, out string json)) return;
    
            (List<ShopData> info, bool ok) = Serializer.DeserializeObject<List<ShopData>>(json);
            if (!ok) return;

            _factory.Clear();
            foreach (ShopData shopData in info)
            {
                var position = shopData.Position.ToVector3();
                var rotation = shopData.Rotation.ToQuaternion();
                if (!_catalog.TryGetSeller(shopData.ConfigName, out SellerConfig config)) continue;

                ShopContext shopContext = _factory.Create(position, rotation, config);
                _npcSellerSerializer.Deserialize(shopContext.Shop, shopData);
            }
        }
    }
}