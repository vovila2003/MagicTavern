using JetBrains.Annotations;
using Modules.Consuming;
using Modules.Inventories;
using Modules.Items;

namespace Tavern.Gardening.Fertilizer
{
    [UsedImplicitly]
    public class FertilizerInventory : 
        StackableInventory<FertilizerItem>,
        IConsumable
    {
        private readonly FertilizerConsumer _consumer;

        public FertilizerInventory()
        {
            _consumer = new FertilizerConsumer(this);
        }

        public void Consume(ItemConfig config, object target)
        {
            _consumer.ConsumeItem(config.Name, target);
        }
    }
}