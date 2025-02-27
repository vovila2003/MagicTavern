using Modules.Consuming;
using Modules.Items;
using Sirenix.OdinInspector;

namespace Tavern.Common
{
    public abstract class ConsumableInventoryContext<T> : StackableInventoryContext<T> where T : Item
    {
        protected ItemConsumer<T> Consumer;

        [Button]
        public void Consume(ItemConfig config, object target)
        {
            Consumer.ConsumeItem(config.Name, target);
        }
    }
}