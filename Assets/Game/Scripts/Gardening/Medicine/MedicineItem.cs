using System;
using Modules.Items;

namespace Tavern.Gardening.Medicine
{
    [Serializable]
    public class MedicineItem : Item
    {
        public MedicineItem(ItemConfig config, params IItemComponent[] attributes) : base(config, attributes) { }

        public override Item Clone()
        {
            return new MedicineItem(Config, GetComponents());
        }
    }
}