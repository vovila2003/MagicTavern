using Modules.Items;

namespace Modules.Shopping
{
    public interface IGoodComponent
    {
        string Name { get; }
        ItemMetadata Metadata { get; }
    }
}