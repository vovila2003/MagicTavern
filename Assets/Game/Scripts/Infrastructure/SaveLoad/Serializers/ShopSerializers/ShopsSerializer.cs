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
        private const string Shops = "Shops";
        private const string Transform = "Transform";
        private const string Config = "Config";
        private const string NpcSeller = "NpcSeller";
        
        private readonly ShopFactory _factory;
        private readonly TransformSerializer _transformSerializer;
        private readonly SellerConfigSerializer _sellerConfigSerializer;
        private readonly NpcSellerSerializer _npcSellerSerializer;

        public ShopsSerializer(ShopFactory factory, GameSettings gameSettings)
        {
            _factory = factory;
            _transformSerializer = new TransformSerializer();
            _sellerConfigSerializer = new SellerConfigSerializer(gameSettings.ShoppingSettings.SellerCatalog);
            _npcSellerSerializer = new NpcSellerSerializer();
        }

        public void Serialize(IDictionary<string, string> saveState)
        {
            var shops = new List<string>(_factory.Shops.Count);
    
            foreach (ShopContext shopContext in _factory.Shops.Keys)
            {
                var info = new Dictionary<string, string>
                {
                    [Transform] = _transformSerializer.Serialize(shopContext.transform),
                    [Config] = _sellerConfigSerializer.Serialize(shopContext.SellerConfig),
                    [NpcSeller] = _npcSellerSerializer.Serialize(shopContext.Shop.NpcSeller)
                };
                shops.Add(Serializer.SerializeObject(info));
            }
    
            saveState[Shops] = Serializer.SerializeObject(shops);
        }

        public void Deserialize(IDictionary<string, string> loadState)
        {
            //TODO
        }
    }
}