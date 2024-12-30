namespace Modules.Gardening
{
    public struct HarvestResult
    {
        public bool IsNormal;
        public int Value;
        public PlantConfig PlantConfig;
        public bool HasSeedInHarvest;
    }
}