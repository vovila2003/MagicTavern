using JetBrains.Annotations;
using Modules.Consuming;
using Modules.Inventories;
using Modules.Items;

namespace Tavern.Gardening.Medicine
{
    [UsedImplicitly]
    public class MedicineInventory : 
        StackableInventory<MedicineItem>, 
        IConsumable
    {
        private readonly MedicineConsumer _consumer;

        public MedicineInventory()
        {
            _consumer = new  MedicineConsumer(this);
        }

        public void Consume(ItemConfig config, object target)
        {
            _consumer.ConsumeItem(config.Name, target);
        }
    }
}