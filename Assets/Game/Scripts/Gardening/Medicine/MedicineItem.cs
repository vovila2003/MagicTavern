using System;
using Modules.Items;

namespace Tavern.Gardening.Medicine
{
    [Serializable]
    public class MedicineItem : Item
    {
        public MedicineItem(ItemConfig config, IItemComponent[] attributes, IExtraItemComponent[] extra)
            : base(config, attributes, extra) { }

        public override Item Clone()
        {
            return new MedicineItem(Config, GetComponents(), GetExtraComponents());
        }
    }
}