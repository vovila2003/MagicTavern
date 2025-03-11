using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.Inventories;
using Modules.Items;
using Unity.Plastic.Newtonsoft.Json;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class ItemSerializer
    {
        private const string Count = "count";
        private const string Extra = "extra";
        private const string Name = "id";

        private readonly Dictionary<string, IExtraSerializer> _extraSerializers;

        public ItemSerializer(Dictionary<string, IExtraSerializer> extraSerializers)
        {
            _extraSerializers = extraSerializers;
        }

        public string Serialize(Item item)
        {
            var data = new Dictionary<string, string>();

            SerializeName(item, data);
            SerializeCount(item, data);
            SerializeExtras(item, data);

            return JsonConvert.SerializeObject(data);
        }

        public Item Deserialize<T>(string itemData, ItemsCatalog catalog) where T : Item
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(itemData);
            if (data == null) return null;
            
            var item = DeserializeItem<T>(data, catalog);
            if (item == null) return null;

            DeserializeCount(item, data);
            DeserializeExtras(item, data);
            
            return item;
        }

        private static void SerializeName(Item item, Dictionary<string,string> data)
        {
            data[Name] = item.ItemName;
        }

        private static void SerializeCount(Item item, Dictionary<string, string> data)
        {
            var value = 1;
            if (item.TryGet(out ComponentStackable stackable))
            {
                value = stackable.Value;
            }
            
            data[Count] = value.ToString();
        }

        private void SerializeExtras(Item item, Dictionary<string, string> data)
        {
            var extras = new Dictionary<string, List<string>>();
            
            foreach (IExtraItemComponent extraComponent in item.ExtraComponents)
            {
                string key = extraComponent.ComponentName;
                if (!extras.ContainsKey(key))
                {
                    extras[key] = new List<string>();
                }

                if (!_extraSerializers.TryGetValue(key, out IExtraSerializer extraSerializer)) continue;
                
                extras[key].Add(extraSerializer.Serialize(extraComponent));
            }

            if (extras.Count == 0) return;

            data[Extra] = JsonConvert.SerializeObject(extras);
        }

        private static T DeserializeItem<T>(Dictionary<string,string> data, ItemsCatalog catalog) where T : Item
        {
            if (!data.TryGetValue(Name, out string name)) return null;
            
            if (!catalog.TryGetItem(name, out ItemConfig itemConfig)) return null;
            
            return itemConfig.Create() as T;
        }

        private static void DeserializeCount(Item item, Dictionary<string, string> data)
        {
            if (!data.TryGetValue(Count, out string countText)) return;

            if (!int.TryParse(countText, out int count)) return;
            
            if (item.TryGet(out ComponentStackable stackable))
            {
                stackable.Value = count;
            }
        }

        private void DeserializeExtras(Item item, Dictionary<string, string> data)
        {
            if (!data.TryGetValue(Extra, out string extraText)) return;
            
            var extras = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(extraText);
            foreach ((string extraName, List<string> values) in extras)
            {
                if (!_extraSerializers.TryGetValue(extraName, out IExtraSerializer extraSerializer)) continue;

                foreach (string value in values)
                {
                    extraSerializer.Deserialize(item, value);
                }
            }
        }
    }
}