using JetBrains.Annotations;
using Modules.Consuming;
using Modules.Inventories;
using Modules.Items;

namespace Tavern.Gardening.Fertilizer
{
    [UsedImplicitly]
    public class FertilizerInventory : StackableInventory<FertilizerItem>, IConsumable<FertilizerItem>
    {
        public ItemConsumer<FertilizerItem> Consumer { get; }

        public FertilizerInventory(FertilizerConsumer consumer)
        {
            Consumer = consumer;
            Consumer.Init(this);
            Consumer.AddHandler(new SeedbedBoostSicknessReducingHandler());
            Consumer.AddHandler(new GrowthAccelerationHandler());
            Consumer.AddHandler(new HarvestBoosterHandler());
            Consumer.AddHandler(new SeedInHarvestHandler());
        }

        public void Consume(ItemConfig config, object target)
        {
            Consumer.ConsumeItem(config.Name, target);
        }
    }
}