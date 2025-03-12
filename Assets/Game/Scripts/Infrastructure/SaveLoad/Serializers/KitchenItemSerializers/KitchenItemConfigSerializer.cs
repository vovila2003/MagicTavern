using Modules.Items;
using Tavern.Cooking;

namespace Tavern.Infrastructure
{
    public class KitchenItemConfigSerializer
    {
        private readonly KitchenItemsCatalog _catalog;

        public KitchenItemConfigSerializer(KitchenItemsCatalog catalog)
        {
            _catalog = catalog;
        }

        public string Serialize(KitchenItemConfig config)
        {
            return config.Name;
        }

        public KitchenItemConfig Deserialize(string value)
        {
            if (!_catalog.TryGetItem(value, out ItemConfig config)) return null;
            
            return config as KitchenItemConfig;
        }
    }
}