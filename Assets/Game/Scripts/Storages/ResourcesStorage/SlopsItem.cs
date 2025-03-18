using Modules.Items;

namespace Tavern.Storages
{
    public class SlopsItem : Item
    {
        public SlopsItem(ItemConfig config, IItemComponent[] attributes, IExtraItemComponent[] extra) 
            : base(config, attributes, extra) { }

        public override Item Clone()
        {
            return new SlopsItem(Config, GetComponents(), GetExtraComponents());
        }
    }
}