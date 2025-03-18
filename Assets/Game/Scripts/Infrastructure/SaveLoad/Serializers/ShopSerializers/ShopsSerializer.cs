using JetBrains.Annotations;
using Tavern.Settings;
using Tavern.Shopping;
using Tavern.Utils;
using UnityEngine;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class ShopsSerializer : GameSerializer<ShopsData>
    {
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

        protected override ShopsData Serialize()
        {
            var data = new ShopsData(_factory.Shops.Count);
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
                data.Shops.Add(shopData);
            }

            return data;
        }

        protected override void Deserialize(ShopsData data)
        {
            _factory.Clear();
            foreach (ShopData shopData in data.Shops)
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