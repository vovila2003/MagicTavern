using JetBrains.Annotations;
using Modules.Consuming;
using Modules.Inventories;
using Modules.Items;

namespace Tavern.Gardening.Medicine
{
    [UsedImplicitly]
    public class MedicineInventory : StackableInventory<MedicineItem>, IConsumable<MedicineItem>
    {
        public ItemConsumer<MedicineItem> Consumer { get; }

        public MedicineInventory(MedicineConsumer consumer)
        {
            Consumer = consumer;
            Consumer.Init(this);
            Consumer.AddHandler(new HarvestHealHandler());
            Consumer.AddHandler(new HarvestSicknessReducingHandler());
        }

        public void Consume(ItemConfig config, object target)
        {
            Consumer.ConsumeItem(config.Name, target);
        }
    }
}