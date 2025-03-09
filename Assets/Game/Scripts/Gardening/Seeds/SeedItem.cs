using System;
using Modules.Items;

namespace Tavern.Gardening
{
    [Serializable]
    public class SeedItem : Item
    {
        public SeedItem(ItemConfig config, IItemComponent[] attributes, IExtraItemComponent[] extra)
            : base(config, attributes, extra) { }

        public override Item Clone()
        {
            return new SeedItem(Config, GetComponents(), GetExtraComponents());
        }
    }
}