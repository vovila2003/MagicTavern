using System;
using Modules.Items;

namespace Tavern.Gardening.Fertilizer
{
    [Serializable]
    public class FertilizerItem : Item
    {
        public FertilizerItem(ItemConfig config, IItemComponent[] attributes, IExtraItemComponent[] extra)
            : base(config, attributes, extra) { }

        public override Item Clone()
        {
            return new FertilizerItem(Config, GetComponents(), GetExtraComponents());
        }
    }
}