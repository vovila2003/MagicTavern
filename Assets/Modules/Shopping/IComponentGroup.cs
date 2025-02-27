using Modules.Items;

namespace Modules.Shopping
{
    public interface IComponentGroup : IItemComponent
    {
        ComponentGroupConfig Config { get; }
    }
}