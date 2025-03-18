using Modules.Gardening;

namespace Tavern.Infrastructure
{
    public class SeedbedSerializer
    {
        private readonly HarvestSerializer _harvestSerializer = new();

        public SeedbedData Serialize(ISeedbed seedbed)
        {
            if (seedbed is null) return null;
            
            return new SeedbedData
            {
                IsBoosted = seedbed.IsBoosted,
                IsSickReduced = seedbed.IsSickReduced,
                IsAccelerated = seedbed.IsAccelerated,
                SeedInHarvestProbability = seedbed.SeedInHarvestProbability,
                HarvestData = _harvestSerializer.Serialize(seedbed.Harvest)
            };
        }

        public void Deserialize(ISeedbed seedbed, SeedbedData data)
        {
            _harvestSerializer.Deserialize(seedbed, seedbed.Harvest, data.HarvestData);
            seedbed.IsBoosted = data.IsBoosted;
            seedbed.IsSickReduced = data.IsSickReduced;
            seedbed.IsAccelerated = data.IsAccelerated;
            seedbed.SeedInHarvestProbability = data.SeedInHarvestProbability;
        }
    }
}