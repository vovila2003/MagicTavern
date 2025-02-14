using System;
using Modules.Items;

namespace Tavern.Cooking
{
    [Serializable]
    public class DishItem : Item
    {
        public DishItem(ItemConfig config, params IItemComponent[] attributes) : base(config, attributes) { }

        public override Item Clone()
        {
            return new DishItem(Config, GetComponents());
        }
    }
}