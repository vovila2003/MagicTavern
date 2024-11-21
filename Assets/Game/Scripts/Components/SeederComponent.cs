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
        private ISeedsStorage _seedsStorage;
        private bool _isEnable;
        private IWaterStorage _waterStorage;

        [Inject]
        private void Construct(ISeedsStorage seedsStorage, IWaterStorage waterStorage)
        {
            _seedsStorage = seedsStorage;
            _waterStorage = waterStorage;
        }

        [Button]
        public void Seed(Seedbed seedbed, PlantConfig plant)
        {
            if (!_isEnable) return;
            
            if (seedbed is null)
            {
                Debug.LogWarning("Seedbed is null");
                return;
            }

            if (plant is null)
            {
                Debug.LogWarning("Plant is null");
                return;
            }
            
            if (!_seedsStorage.TryGetStorage(plant.Plant, out PlantStorage storage))
            {
                Debug.Log("Seed storage of type {type} is not found!");
                return;
            }

            const int count = 1;

            if (!storage.CanSpend(count))
            {
                Debug.Log("Not enough seeds of type {type} in storage!");
                return;
            }

            bool result = seedbed.Seed(plant);
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
        public void Watering(Seedbed seedbed)
        {
            if (!_isEnable) return;
            
            if (seedbed is null)
            {
                Debug.LogWarning("Seedbed is null");
                return;
            }
            
            const int count = 1;

            if (_waterStorage.Value < count)
            {
                Debug.Log("Not enough water in storage!");
                return;
            }

            seedbed.Watering();
            _waterStorage.Spend(count);
        }

        void IStartGameListener.OnStart() => _isEnable = true;

        void IFinishGameListener.OnFinish() => _isEnable = false;

        void IPauseGameListener.OnPause() => _isEnable = false;

        void IResumeGameListener.OnResume() => _isEnable = true;

        void IInitGameListener.OnInit() => _isEnable = false;
    }
}