using Modules.Gardening;
using Tavern.ProductsAndIngredients;
using Tavern.Storages;

namespace Tavern.Gardening
{
    public sealed class PotHarvestController
    {
        private readonly Pot _pot;
        private readonly PlantProductInventoryContext _plantProductsStorage;
        private readonly ISlopsStorage _slopeStorage;
        private readonly SeedInventoryContext _seedsStorage;

       public PotHarvestController(
            Pot pot, 
            PlantProductInventoryContext plantProductsStorage, 
            ISlopsStorage slopeStorage, 
            SeedInventoryContext seedsStorage)
        {
            _pot = pot;
            _plantProductsStorage = plantProductsStorage;
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
            _plantProductsStorage.AddItemByName(PlantProductNameProvider.GetName(config.Name), value);
        }

        private void OnSlopsReceived(int value)
        {
            _slopeStorage.AddSlops(value);
        }
    }
}