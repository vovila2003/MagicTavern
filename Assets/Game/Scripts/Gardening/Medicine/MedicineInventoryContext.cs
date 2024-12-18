using Tavern.Common;
using VContainer;

namespace Tavern.Gardening.Medicine
{
    public class MedicineInventoryContext : ConsumableInventoryContext<MedicineItem>
    {
        [Inject]
        private void Construct(MedicineConsumer consumer)
        {
            Consumer = consumer;
            Consumer.AddHandler(new HarvestHealHandler());
            Consumer.AddHandler(new HarvestSicknessReducingHandler());
        }
    }
}