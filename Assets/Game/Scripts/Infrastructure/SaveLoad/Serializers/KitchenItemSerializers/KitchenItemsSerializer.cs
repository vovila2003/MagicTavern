using JetBrains.Annotations;
using Modules.Items;
using Tavern.Cooking;
using Tavern.Settings;
using Tavern.Utils;
using UnityEngine;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class KitchenItemsSerializer : GameSerializer<KitchenItemsData>
    {
        private readonly KitchenItemFactory _factory;
        private readonly KitchenItemsCatalog _catalog;

        public KitchenItemsSerializer(KitchenItemFactory factory, GameSettings settings)
        {
            _factory = factory;
            _catalog = settings.CookingSettings.KitchenItemCatalog;
        }
    
        protected override KitchenItemsData Serialize()
        {
            var data = new KitchenItemsData(_factory.KitchenItems.Count);
    
            foreach (KitchenItemContext kitchenItem in _factory.KitchenItems.Keys)
            {
                Transform transform = kitchenItem.transform;
                var info = new KitchenItemData
                {
                    Position = transform.position.ToFloat3(),
                    Rotation = transform.rotation.ToFloat4(),
                    ConfigName = kitchenItem.KitchenItemConfig.Name
                };

                data.Items.Add(info);
            }

            return data;
        }

        protected override void Deserialize(KitchenItemsData data)
        {
            _factory.Clear();
            foreach (KitchenItemData itemData in data.Items)
            {
                var position = itemData.Position.ToVector3();
                var rotation = itemData.Rotation.ToQuaternion();
                
                if (!_catalog.TryGetItem(itemData.ConfigName, out ItemConfig config)) continue;

                if (config is not KitchenItemConfig kitchenConfig) continue;
                
                _factory.Create(position, rotation, kitchenConfig);
            }
        }
    }
}