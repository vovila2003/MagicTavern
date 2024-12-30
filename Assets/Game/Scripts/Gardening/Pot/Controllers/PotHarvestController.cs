using Modules.Gardening;
using Tavern.Storages;

namespace Tavern.Gardening
{
    public sealed class PotHarvestController
    {
        private readonly Pot _pot;
        private readonly ProductInventoryContext _productsStorage;
        private readonly ISlopsStorage _slopeStorage;
        private readonly SeedInventoryContext _seedsStorage;

       public PotHarvestController(
            Pot pot, 
            ProductInventoryContext productsStorage, 
            ISlopsStorage slopeStorage, 
            SeedInventoryContext seedsStorage)
        {
            _pot = pot;
            _productsStorage = productsStorage;
            _slopeStorage = slopeStorage;
            _seedsStorage = seedsStorage;
            _pot.OnHarvestReceived += OnHarvestReceived;
            _pot.OnSlopsReceived += OnSlopsReceived;
        }

        public void Dispose()
        {
            _pot.OnHarvestReceived -= OnHarvestReceived;
            _pot.OnSlopsReceived -= OnSlopsReceived;
        }

        private void OnHarvestReceived(PlantConfig config, int value, bool hasSeed)
        {
            AddHarvestToProductStorage(config, value);

            if (hasSeed)
            {
                _seedsStorage.AddItemByName(SeedNameProvider.GetName(config.Name));
            }
        }

        private void AddHarvestToProductStorage(PlantConfig config, int value)
        {
            for (var i = 0; i < value; i++)
            {
                _productsStorage.AddItemByName(ProductNameProvider.GetName(config.Name));
            }
        }

        private void OnSlopsReceived(int value)
        {
            _slopeStorage.AddSlops(value);
        }
    }
}