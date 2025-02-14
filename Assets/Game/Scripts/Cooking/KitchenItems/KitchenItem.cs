using System;
using Modules.Items;

namespace Tavern.Cooking
{
    [Serializable]
    public class KitchenItem : Item
    {
        public KitchenItem(ItemConfig config, params IItemComponent[] attributes) : base(config, attributes) { }

        public override Item Clone()
        {
            return new KitchenItem(Config, GetComponents());
        }
    }
}