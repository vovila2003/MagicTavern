using JetBrains.Annotations;
using Modules.Consuming;
using Modules.Inventories;

namespace Tavern.Gardening.Fertilizer
{
    [UsedImplicitly]
    public class FertilizerConsumer : ItemConsumer<FertilizerItem>
    {
        public FertilizerConsumer(IInventory<FertilizerItem> inventory) : base(inventory)
        {
            AddHandler(new SeedbedBoostSicknessReducingHandler());
            AddHandler(new GrowthAccelerationHandler());
            AddHandler(new HarvestBoosterHandler());
            AddHandler(new SeedInHarvestHandler());
        }
    }
}