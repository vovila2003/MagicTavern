using System;
using Modules.Items;

namespace Tavern.Cooking
{
    [Serializable]
    public class KitchenItem : Item
    {
        public KitchenItem(ItemConfig config, IItemComponent[] attributes, IExtraItemComponent[] extra)
            : base(config, attributes, extra) { }

        public override Item Clone()
        {
            return new KitchenItem(Config, GetComponents(), GetExtraComponents());
        }
    }
}