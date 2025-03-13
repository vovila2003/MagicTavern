using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Items;
using Modules.SaveLoad;
using Tavern.Cooking;
using Tavern.Settings;
using Tavern.Utils;
using UnityEngine;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class KitchenItemsSerializer : IGameSerializer
    {
        [Serializable]
        public class KitchenItemData
        {
            public float[] Position;
            public float[] Rotation;
            public string ConfigName;
        }
        
        private const string KitchenItems = "KitchenItems";
        
        private readonly KitchenItemFactory _factory;
        private readonly KitchenItemsCatalog _catalog;

        public KitchenItemsSerializer(KitchenItemFactory factory, GameSettings settings)
        {
            _factory = factory;
            _catalog = settings.CookingSettings.KitchenItemCatalog;
        }
    
        public void Serialize(IDictionary<string, string> saveState)
        {
            var items = new List<KitchenItemData>(_factory.KitchenItems.Count);
    
            foreach (KitchenItemContext kitchenItem in _factory.KitchenItems.Keys)
            {
                Transform transform = kitchenItem.transform;
                var info = new KitchenItemData
                {
                    Position = transform.position.ToFloat3(),
                    Rotation = transform.rotation.ToFloat4(),
                    ConfigName = kitchenItem.KitchenItemConfig.Name
                };

                items.Add(info);
            }
    
            saveState[KitchenItems] = Serializer.SerializeObject(items);
        }
    
        public void Deserialize(IDictionary<string, string> loadState)
        {
            if (!loadState.TryGetValue(KitchenItems, out string json)) return;
    
            (List<KitchenItemData> info, bool ok) = Serializer.DeserializeObject<List<KitchenItemData>>(json);
            if (!ok) return;
    
            _factory.Clear();
            foreach (KitchenItemData itemData in info)
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