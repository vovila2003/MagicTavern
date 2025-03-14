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
                SickProbability = harvest.SickProbability,
                Value = harvest.Value,
                HarvestState = harvest.State,
                HarvestAge = harvest.Age,
                PlantConfigName = harvest.PlantConfig.Name,
                GrowthTimerData = _timerSerializer.Serialize(harvest.GrowthTimer),
                HarvestWateringData = _wateringSerializer.Serialize(harvest.HarvestWatering),
                HarvestSicknessData = _sicknessSerializer.Serialize(harvest.HarvestSickness),
            };
        }
    }
}