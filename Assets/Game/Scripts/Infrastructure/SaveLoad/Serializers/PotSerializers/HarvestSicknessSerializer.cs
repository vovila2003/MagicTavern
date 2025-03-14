using Modules.Gardening;

namespace Tavern.Infrastructure
{
    public class HarvestSicknessSerializer
    {
        public HarvestSicknessData Serialize(HarvestSickness harvestSickness) =>
            new()
            {
                Probability = harvestSickness.Probability
            };
    }
}