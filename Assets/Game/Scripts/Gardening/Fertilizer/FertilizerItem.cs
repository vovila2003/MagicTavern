using System;
using Modules.Items;

namespace Tavern.Gardening.Fertilizer
{
    [Serializable]
    public class FertilizerItem : Item
    {
        public FertilizerItem(ItemConfig config, params IItemComponent[] attributes) : base(config, attributes) { }

        public override Item Clone()
        {
            return new FertilizerItem(Config, GetComponents());
        }
    }
}