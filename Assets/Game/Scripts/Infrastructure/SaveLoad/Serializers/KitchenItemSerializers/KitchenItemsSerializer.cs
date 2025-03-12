using System.Collections.Generic;
using JetBrains.Annotations;
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
        private const string KitchenItems = "KitchenItems";
        private const string Transform = "Transform";
        private const string Configs = "Configs";
        
        private readonly KitchenItemFactory _factory;
        private readonly TransformSerializer _transformSerializer;
        private readonly KitchenItemConfigSerializer _kitchenItemConfigSerializer;
        
        public KitchenItemsSerializer(KitchenItemFactory factory, GameSettings settings)
        {
            _factory = factory;
            _transformSerializer = new TransformSerializer();
            _kitchenItemConfigSerializer = new KitchenItemConfigSerializer(settings.CookingSettings.KitchenItemCatalog);
        }
    
        public void Serialize(IDictionary<string, string> saveState)
        {
            var items = new List<string>(_factory.KitchenItems.Count);
    
            foreach (KitchenItemContext kitchenItem in _factory.KitchenItems.Keys)
            {
                var info = new Dictionary<string, string>
                {
                    [Transform] = _transformSerializer.Serialize(kitchenItem.transform),
                    [Configs] = _kitchenItemConfigSerializer.Serialize(kitchenItem.KitchenItemConfig)
                };
                items.Add(Serializer.SerializeObject(info));
            }
    
            saveState[KitchenItems] = Serializer.SerializeObject(items);
        }
    
        public void Deserialize(IDictionary<string, string> loadState)
        {
            if (!loadState.TryGetValue(KitchenItems, out string json)) return;
    
            var info = Serializer.DeserializeObject<List<string>>(json);
            if (info == null) return;
    
            _factory.Clear();
            foreach (string kitchenItemInfoString in info)
            {
                var kitchenInfo = Serializer.DeserializeObject<Dictionary<string, string>>(kitchenItemInfoString);
                if (kitchenInfo == null) continue;
    
                Vector3 position = Vector3.zero;
                Quaternion rotation = Quaternion.identity;
                if (kitchenInfo.TryGetValue(Transform, out string transformString))
                {
                    (position, rotation) = _transformSerializer.Deserialize(transformString);
                }
    
                if (!kitchenInfo.TryGetValue(Configs, out string configString)) continue;
                
                KitchenItemConfig config = _kitchenItemConfigSerializer.Deserialize(configString);
                if (config == null) continue;
                
                _factory.Create(position, rotation, config);
            }
        }
    }
}