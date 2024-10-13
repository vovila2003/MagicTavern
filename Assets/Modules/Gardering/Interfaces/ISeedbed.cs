using Gardering.Implementations;

namespace Gardering.Interfaces
{
    public interface ISeedbed
    {
        bool Prepare();
        bool Seed(SeedConfig seed);
        public bool Gather(out IHarvest harvest);
    }
}