using System;
using Modules.Items;

namespace Tavern.Looting
{
    [Serializable]
    public class LootItem : Item
    {
        public LootItem(ItemConfig config, params IItemComponent[] attributes) : base(config, attributes) { }

        public override Item Clone()
        {
            return new LootItem(Config, GetComponents());
        }
    }
}