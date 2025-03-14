using Modules.Gardening;

namespace Tavern.Infrastructure
{
    public class SeedbedSerializer
    {
        private readonly HarvestSerializer _harvestSerializer = new();

        public SeedbedData Serialize(ISeedbed seedbed)
        {
            if (seedbed is null) return null;
            
            return new SeedbedData()
            {
                IsBoosted = seedbed.IsBoosted,
                IsSickReduced = seedbed.IsSickReduced,
                IsAccelerated = seedbed.IsAccelerated,
                SeedInHarvestProbability = seedbed.SeedInHarvestProbability,
                HarvestData = _harvestSerializer.Serialize(seedbed.Harvest)
            };
        }
    }
}