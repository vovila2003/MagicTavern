using Modules.Gardening;

namespace Tavern.Infrastructure
{
    public class HarvestSerializer
    {
        private readonly TimerSerializer _timerSerializer = new();
        private readonly HarvestWateringSerializer _wateringSerializer = new();
        private readonly HarvestSicknessSerializer _sicknessSerializer = new();

        public HarvestData Serialize(IHarvest harvest)
        {
            if (harvest is null) return null;

            return new HarvestData
            {
                IsPaused = harvest.IsPaused,
                IsReadyAfterWatering = harvest.IsReadyAfterWatering,
                IsPenalized = harvest.IsPenalized,
                IsSick = harvest.IsSick,
                IsWaterRequired = harvest.IsWaterRequired,
                ResultHarvestAmount = harvest.ResultHarvestAmount,
                Value = harvest.Value,
                HarvestState = harvest.State,
                HarvestAge = harvest.Age,
                GrowthTimerData = _timerSerializer.Serialize(harvest.GrowthTimer),
                HarvestWateringData = _wateringSerializer.Serialize(harvest.HarvestWatering),
                HarvestSicknessData = _sicknessSerializer.Serialize(harvest.HarvestSickness),
            };
        }

        public void Deserialize(ISeedbed seedbed, IHarvest harvest, HarvestData data)
        {
            //Order is important
            harvest.IsPaused = data.IsPaused;
            harvest.IsReadyAfterWatering = data.IsReadyAfterWatering;
            harvest.IsPenalized = data.IsPenalized;
            harvest.ResultHarvestAmount = data.ResultHarvestAmount;
            harvest.Value = data.Value;
            
            harvest.IsSick = data.IsSick;
            if (!data.IsSick)
            {
                seedbed.Heal();
            }
            
            harvest.SetIsWaterRequired(data.IsWaterRequired);
            _timerSerializer.Deserialize(harvest.GrowthTimer, data.GrowthTimerData);
            _wateringSerializer.Deserialize(harvest.HarvestWatering, data.HarvestWateringData);
            _sicknessSerializer.Deserialize(harvest.HarvestSickness, data.HarvestSicknessData);
            harvest.SetAge(data.HarvestAge); // Age before State
            harvest.SetState(data.HarvestState);
        }
    }
}