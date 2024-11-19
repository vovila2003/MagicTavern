using Modules.GameCycle.Interfaces;
using Modules.Gardening;
using Sirenix.OdinInspector;
using Tavern.Gardening;
using Tavern.Storages;
using UnityEngine;
using VContainer;

namespace Tavern.Components
{
    public class SeederComponent :
        MonoBehaviour,
        IInitGameListener,
        IStartGameListener,
        IFinishGameListener,
        IPauseGameListener,
        IResumeGameListener
    {
        private PlantsCatalog _catalog;
        private ISeedsStorage _seedsStorage;
        private IResourcesStorage _resourcesStorage;
        private SeedbedFactory _factory;
        private bool _isEnable;

        [Inject]
        private void Construct(PlantsCatalog catalog, ISeedsStorage seedsStorage, 
            IResourcesStorage resourcesStorage, SeedbedFactory factory)
        {
            _catalog = catalog;
            _seedsStorage = seedsStorage;
            _resourcesStorage = resourcesStorage;
            _factory = factory;
        }

        [Button]
        public void CreateSeedbed(Vector3 position, Quaternion rotation)
        {
            if (!_isEnable) return;
            
            _factory.CreateSeedbed(position, rotation);
        }

        [Button]
        public void Prepare(Seedbed seedbed)
        {
            if (!_isEnable) return;

            if (seedbed is null)
            {
                Debug.LogWarning("Seedbed is null");
                return;
            }
            
            seedbed.Prepare();
        }

        [Button]
        public void Seed(Seedbed seedbed, PlantConfig plant, int count)
        {
            if (!_isEnable) return;
            
            if (count <= 0) return;
            
            if (seedbed is null)
            {
                Debug.LogWarning("Seedbed is null");
                return;
            }
            
            if (!_catalog.TryGetPlant(plant.Plant, out PlantConfig seedConfig))
            {
                Debug.Log($"Seeds of type {plant.Name} are not found in catalog");
                return;
            }

            if (!_seedsStorage.TryGetStorage(plant.Plant, out PlantStorage storage))
            {
                Debug.Log("Seed storage of type {type} is not found!");
                return;
            }

            if (!storage.CanSpend(count))
            {
                Debug.Log("Not enough seeds of type {type} in storage!");
                return;
            }

            if (!storage.CanSpend(count)) return;
            
            bool result = seedbed.Seed(seedConfig, count);
            if (!result) return;
            
            storage.Spend(count);
        }
        
        [Button]
        public void Gather(Seedbed seedbed)
        {
            if (!_isEnable) return;
            
            if (seedbed is null)
            {
                Debug.LogWarning("Seedbed is null");
                return;
            }
            
            seedbed.Gather();
        }

        [Button]
        public void Care(Seedbed seedbed, Caring caringType)
        {
            if (!_isEnable) return;
            
            if (seedbed is null)
            {
                Debug.LogWarning("Seedbed is null");
                return;
            }

            if (seedbed.CurrentSeedConfig is null) return;

            if (!seedbed.CurrentSeedConfig.TryGetCaring(caringType, out CaringConfig caringSettings)) return;

            float count = caringSettings.CaringValue;
            if (count <= 0)
            {
                seedbed.Care(caringType);
                return;
            }

            if (!_resourcesStorage.TryGetStorage(caringType, out ResourceStorage storage))
            {
                Debug.Log($"Resource storage of type {caringType} is not found!");
                return;
            }

            if (!storage.CanSpend(count))
            {
                Debug.Log($"Not enough resource of type {caringType} in storage!");
                return;
            }

            if (!storage.Spend(count)) return;
            
            seedbed.Care(caringType);
        }

        void IStartGameListener.OnStart()
        {
            _isEnable = true;
        }

        void IFinishGameListener.OnFinish()
        {
            _isEnable = false;
        }

        void IPauseGameListener.OnPause()
        {
            _isEnable = false;
        }

        void IResumeGameListener.OnResume()
        {
            _isEnable = true;
        }

        void IInitGameListener.OnInit()
        {
            _isEnable = false;
        }
    }
}