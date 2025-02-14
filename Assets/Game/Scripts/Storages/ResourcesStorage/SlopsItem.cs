using Modules.Items;

namespace Tavern.Storages
{
    public class SlopsItem : Item
    {
        public SlopsItem(ItemConfig config, params IItemComponent[] attributes) 
            : base(config, attributes) { }

        public override Item Clone()
        {
            return new SlopsItem(Config, GetComponents());
        }
    }
}