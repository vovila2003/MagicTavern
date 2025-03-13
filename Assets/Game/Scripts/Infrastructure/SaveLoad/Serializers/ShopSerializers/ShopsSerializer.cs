using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.SaveLoad;
using Tavern.Settings;
using Tavern.Shopping;
using Tavern.Utils;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class ShopsSerializer : IGameSerializer
    {
        [Serializable]
        public class ItemsData
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
            public List<ItemsData> Items;
            public List<ItemsData> CharacterItems;
        }
        
        private const string Shops = "Shops";
        
        private readonly ShopFactory _factory;
        private readonly NpcSellerDataFiller _npcSellerDataFiller;
        private readonly SellerCatalog _catalog;

        public ShopsSerializer(ShopFactory factory, GameSettings gameSettings)
        {
            _factory = factory;
            _catalog = gameSettings.ShoppingSettings.SellerCatalog;
            _npcSellerDataFiller = new NpcSellerDataFiller();
        }

        public void Serialize(IDictionary<string, string> saveState)
        {
            var shops = new List<ShopData>(_factory.Shops.Count);
    
            foreach (ShopContext shopContext in _factory.Shops.Keys)
            {
                var transform = shopContext.transform;
                var shopData = new ShopData
                {
                    Position = transform.position.ToFloat3(),
                    Rotation = transform.rotation.ToFloat4(),
                    ConfigName = shopContext.SellerConfig.Name
                };
                _npcSellerDataFiller.FillData(shopContext.Shop.NpcSeller, shopData);
                shops.Add(shopData);
            }
    
            saveState[Shops] = Serializer.SerializeObject(shops);
        }

        public void Deserialize(IDictionary<string, string> loadState)
        {
            //TODO
        }
    }
}