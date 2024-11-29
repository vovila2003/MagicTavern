using Modules.Gardening;
using Tavern.Storages;
using UnityEngine;
using VContainer;

namespace Tavern.Gardening
{
    [RequireComponent(typeof(Pot))]
    public sealed class SeedbedHarvestController : MonoBehaviour
    {
        private Pot _pot;
        private IProductsStorage _productsStorage;
        private ISlopsStorage _slopeStorage;
        private ISeedsStorage _seedsStorage;

        [Inject]
        public void Construct(IProductsStorage productsStorage, ISlopsStorage slopeStorage, ISeedsStorage seedsStorage)
        {
            _productsStorage = productsStorage;
            _slopeStorage = slopeStorage;
            _seedsStorage = seedsStorage;
        }

        private void Awake()
        {
            _pot = GetComponent<Pot>();
        }

        private void OnEnable()
        {
            _pot.OnHarvestReceived += OnHarvestReceived;
            _pot.OnSlopsReceived += OnSlopsReceived;
        }

        private void OnDisable()
        {
            _pot.OnHarvestReceived -= OnHarvestReceived;
                        
        }

        private void OnHarvestReceived(Plant type, int value, bool hasSeed)
        {
            AddHarvestToProductStorage(type, value);

            if (hasSeed)
            {
                AddSeedToStorage(type, 1); //one seed in harvest
            }
        }

        private void AddHarvestToProductStorage(Plant type, int value)
        {
            if (!_productsStorage.TryGetStorage(type, out PlantStorage storage))
            {
                Debug.LogWarning($"Unknown storage of type {type.PlantName}");
                return;
            }

            storage.Add(value);
        }

        private void AddSeedToStorage(Plant type, int count)
        {
            if (!_seedsStorage.TryGetStorage(type, out PlantStorage storage))
            {
                Debug.LogWarning($"Unknown seed storage of type {type.PlantName}");
                return;
            }
            
            storage.Add(count);
        }

        private void OnSlopsReceived(int value)
        {
            _slopeStorage.Add(value);
        }
    }
}