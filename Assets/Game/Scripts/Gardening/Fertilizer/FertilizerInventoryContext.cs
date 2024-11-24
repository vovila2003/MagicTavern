using Tavern.Common;
using VContainer;

namespace Tavern.Gardening.Fertilizer
{
    public class FertilizerInventoryContext : ConsumableInventoryContext<FertilizerItem>
    {
        [Inject]
        private void Construct(FertilizerConsumer consumer)
        {
            Consumer = consumer;
            Consumer.AddHandler(new SeedbedBoostSicknessReducingHandler());
            Consumer.AddHandler(new GrowthAccelerationHandler());
            Consumer.AddHandler(new HarvestBoosterHandler());
        }
    }
}