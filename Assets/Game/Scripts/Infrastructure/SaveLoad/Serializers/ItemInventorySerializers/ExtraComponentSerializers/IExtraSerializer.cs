using Modules.Items;

namespace Tavern.Infrastructure
{
    public interface IExtraSerializer
    {
        string Serialize(IExtraItemComponent extraComponent);
        void Deserialize(Item item, string value);
    }
}