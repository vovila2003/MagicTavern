using JetBrains.Annotations;
using Modules.Consuming;
using Modules.Inventories;

namespace Tavern.Gardening.Medicine
{
    [UsedImplicitly]
    public class MedicineConsumer : ItemConsumer<MedicineItem>
    {
        public MedicineConsumer(IInventory<MedicineItem> inventory) : base(inventory)
        {
            AddHandler(new HarvestHealHandler());
            AddHandler(new HarvestSicknessReducingHandler());
        }
    }
}