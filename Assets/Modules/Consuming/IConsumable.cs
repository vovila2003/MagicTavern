using Modules.Items;

namespace Modules.Consuming
{
    public interface IConsumable
    {
        void Consume(ItemConfig config, object target);
    }
}