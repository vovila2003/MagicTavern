using System;
using Modules.Items;
using Sirenix.OdinInspector;

namespace Tavern.Cooking
{
    [Serializable]
    public class DishItem : Item, IExtraItem
    {
        [ShowInInspector, ReadOnly]
        public bool IsExtra { get; set; }

        public DishItem(ItemConfig config, params IItemComponent[] attributes) : base(config, attributes) { }


        public override Item Clone()
        {
            return new DishItem(Config, GetComponents());
        }
    }
}