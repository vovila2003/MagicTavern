using System;
using Modules.Items;

namespace Tavern.Gardening
{
    [Serializable]
    public class SeedItem : Item
    {
        public SeedItem(ItemConfig config, params IItemComponent[] attributes) : base(config, attributes) { }

        public override Item Clone()
        {
            return new SeedItem(Config, GetComponents());
        }
    }
}