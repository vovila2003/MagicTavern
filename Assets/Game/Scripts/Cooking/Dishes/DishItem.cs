using System;
using Modules.Items;

namespace Tavern.Cooking
{
    [Serializable]
    public class DishItem : Item
    {
        public DishItem(ItemConfig config, IItemComponent[] attributes, IExtraItemComponent[] extra) 
            : base(config, attributes, extra) { }

        public override Item Clone()
        {
            return new DishItem(Config, GetComponents(), GetExtraComponents());
        }
    }
}