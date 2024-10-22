using System;
using Modules.Gardening;
using Sirenix.OdinInspector;
using Tavern.Architecture.GameManager.Interfaces;
using UnityEngine;

namespace Tavern.Gardening
{
    public class SeedbedTest : 
        MonoBehaviour,
        IStartGameListener,
        IPauseGameListener,
        IResumeGameListener,
        IFinishGameListener
    {
        public event Action<PlantType, int> OnHarvestReceived;
        
        private readonly ISeedbed _seedbed = new Seedbed();
        private bool _isEnable;
        
        private void OnEnable()
        {
            _seedbed.OnStateChanged += OnStateChanged;
            _seedbed.OnHarvestStateChanged += OnHarvestStateChanged;
            _seedbed.OnCaringChanged += OnCaringChanged;
        }
        
        private void OnDisable()
        {
            _seedbed.OnStateChanged -= OnStateChanged;
            _seedbed.OnHarvestStateChanged -= OnHarvestStateChanged;
            _seedbed.OnCaringChanged -= OnCaringChanged;
        }

        private void Update()
        {
            if (!_isEnable) return;
            
            _seedbed.Tick(Time.deltaTime);
        }

        [Button]
        public void Prepare()
        {
            if (!_isEnable) return;
            
            bool result = _seedbed.Prepare();
            Debug.Log($"Prepare seedbed: {result}");
        }

        [Button]
        public void Seed(SeedConfig seedConfig)
        {
            if (!_isEnable) return;
            
            if (seedConfig is null) return;
            
            bool result = _seedbed.Seed(seedConfig);
            Debug.Log($"Seeded: {result}");
        }
        
        [Button]
        public void Gather()
        {
            if (!_isEnable) return;
            
            bool result = _seedbed.Gather(out HarvestResult harvestResult);
            Debug.Log($"Gather: {result}.");
            if (result)
            {
                Debug.Log($"HarvestResult: {harvestResult.IsCollected}, {harvestResult.Value}, {harvestResult.Type}");
                if (harvestResult.IsCollected)
                {
                    OnHarvestReceived?.Invoke(harvestResult.Type, harvestResult.Value);
                }
            }
        }

        [Button]
        public void Care(CaringType caringType)
        {
            if (!_isEnable) return;
            
            _seedbed.Care(caringType);
        }
        
        void IPauseGameListener.OnPause()
        {
            _isEnable = false;
            _seedbed.Pause();
        }

        void IResumeGameListener.OnResume()
        {
            _isEnable = true;
            _seedbed.Resume();
        }

        void IStartGameListener.OnStart()
        {
            _isEnable = true;
        }

        void IFinishGameListener.OnFinish()
        {
            _isEnable = false;
            _seedbed.Stop();
        }

        private void OnStateChanged(SeedbedState state)
        {
            Debug.Log($"Seedbed state changed to {state}");
        }

        private void OnHarvestStateChanged(HarvestState state)
        {
            Debug.Log($"Harvest state changed to {state}");
            if (state == HarvestState.Lost)
            {
                Debug.Log($"Lost by reason {_seedbed.LostReason}");
            }
        }

        private void OnCaringChanged(CaringType type, CaringState caringState)
        {
            Debug.Log($"Care {type} state changed to {caringState}!");
        }
    }
}