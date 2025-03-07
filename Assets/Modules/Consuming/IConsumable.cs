using Modules.Items;

namespace Modules.Consuming
{
    public interface IConsumable<T> where T : Item
    {
        ItemConsumer<T> Consumer { get; }
        void Consume(ItemConfig config, object target);
    }
}