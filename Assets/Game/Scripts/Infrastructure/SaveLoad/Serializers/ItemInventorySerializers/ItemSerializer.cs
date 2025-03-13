using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Inventories;
using Modules.Items;
using Tavern.Utils;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class ItemSerializer
    {
        [Serializable]
        public class ExtraData
        {
            public string Name;
            public string Data;
        }
        
        [Serializable]
        public class ItemData
        {
            public string Name;
            public int Count;
            public List<ExtraData> Extras;
        }
        
        private readonly Dictionary<string, IExtraSerializer> _extraSerializers;

        public ItemSerializer(Dictionary<string, IExtraSerializer> extraSerializers)
        {
            _extraSerializers = extraSerializers;
        }

        public ItemData Serialize(Item item)
        {
            var data = new ItemData
            {
                Name = item.ItemName,
                Count = item.GetCount(),
                Extras = SerializeExtras(item)
            };

            return data;
        }

        public Item Deserialize<T>(ItemData itemData, ItemsCatalog catalog) where T : Item
        {
            var item = DeserializeItem<T>(itemData, catalog);
            if (item == null) return null;

            DeserializeCount(item, itemData.Count);
            DeserializeExtras(item, itemData.Extras);
            
            return item;
        }

        private List<ExtraData> SerializeExtras(Item item)
        {
            var extras = new List<ExtraData>();
            
            foreach (IExtraItemComponent extraComponent in item.ExtraComponents)
            {
                var extraData = new ExtraData
                {
                    Name = extraComponent.ComponentName
                };

                if (!_extraSerializers.TryGetValue(extraData.Name, out IExtraSerializer extraSerializer)) continue;

                extraData.Data = extraSerializer.Serialize(extraComponent); 
                extras.Add(extraData);
            }

            return extras;
        }

        private static T DeserializeItem<T>(ItemData data, ItemsCatalog catalog) where T : Item
        {
            if (!catalog.TryGetItem(data.Name, out ItemConfig itemConfig)) return null;
            
            return itemConfig.Create() as T;
        }

        private static void DeserializeCount(Item item, int count)
        {
            if (item.TryGet(out ComponentStackable stackable))
            {
                stackable.Value = count;
            }
        }

        private void DeserializeExtras(Item item, List<ExtraData> data)
        {
            foreach (ExtraData extraData in data)
            {
                if (!_extraSerializers.TryGetValue(extraData.Name, out IExtraSerializer extraSerializer)) continue;

                extraSerializer.Deserialize(item, extraData.Data);
            }
        }
    }
}