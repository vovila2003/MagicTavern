using System;
using Modules.Items;

namespace Tavern.Looting
{
    [Serializable]
    public class LootItem : Item
    {
        public LootItem(ItemConfig config, IItemComponent[] attributes, IExtraItemComponent[] extra)
            : base(config, attributes, extra) { }

        public override Item Clone()
        {
            return new LootItem(Config, GetComponents(), GetExtraComponents());
        }
    }
}