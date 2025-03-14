using Modules.Gardening;

namespace Tavern.Infrastructure
{
    public class HarvestWateringSerializer
    {
        private readonly TimerSerializer _timerSerializer = new();
        
        public HarvestWateringData Serialize(HarvestWatering harvestWatering) =>
            new()
            {
                BaseDryingTimerDuration = harvestWatering.BaseDryingTimerDuration,
                WateringTimerData = _timerSerializer.Serialize(harvestWatering.WateringTimer),
                DryingTimerData = _timerSerializer.Serialize(harvestWatering.DryingTimer)
            };
    }
}