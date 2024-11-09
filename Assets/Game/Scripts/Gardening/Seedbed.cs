using System;
using Modules.Gardening;
using Sirenix.OdinInspector;
using Tavern.Architecture.GameManager.Interfaces;
using UnityEngine;

namespace Tavern.Gardening
{
    public class Seedbed :
        MonoBehaviour,
        IStartGameListener,
        IPauseGameListener,
        IResumeGameListener,
        IFinishGameListener,
        IExitGameListener
    {
        public event Action<PlantType, int> OnHarvestReceived;
        public event Action<Seedbed> OnDestroyed;

        private readonly ISeedbed _seedbed = new SeedbedImpl();
        private bool _isEnable;
        private int _count;

        [ShowInInspector, ReadOnly]
        public SeedbedState SeedbedState => _seedbed.State;
        
        [ShowInInspector, ReadOnly]
        public HarvestState HarvestState { get; private set; } = HarvestState.NorReady;

        private void Awake()
        {
            _isEnable = true;
        }

        private void OnEnable()
        {
            _seedbed.OnStateChanged += OnStateChanged;
            _seedbed.OnHarvestStateChanged += OnHarvestStateChanged;
            _seedbed.OnCaringChanged += OnCaringChanged;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void Update()
        {
            if (!_isEnable) return;
            
            _seedbed.Tick(Time.deltaTime);
        }

        public void Prepare()
        {
            if (!_isEnable) return;
            
            bool result = _seedbed.Prepare();
            Debug.Log($"Prepare seedbed: {result}");
        }

        public bool Seed(SeedConfig seedConfig, int count)
        {
            if (!_isEnable) return false;

            if (seedConfig is null) return false; 
            
            bool result = _seedbed.Seed(seedConfig);
            Debug.Log($"Seedbed seeded: {result}");

            if (result)
            {
                _count = count;
            }

            return result;
        }

        public void Gather()
        {
            if (!_isEnable) return;
            
            bool gathered = _seedbed.Gather(out HarvestResult harvestResult);
            Debug.Log($"Seedbed gathered: {gathered}.");
            if (!gathered) return;
            
            Debug.Log($"HarvestResult: {harvestResult.IsCollected}, " +
                      $"{harvestResult.Value}, {harvestResult.Type}");
            
            if (!harvestResult.IsCollected) return;
            
            OnHarvestReceived?.Invoke(harvestResult.Type, harvestResult.Value * _count);
            _count = 0;
        }

        public void Care(CaringType caringType)
        {
            if (!_isEnable) return;
            
            _seedbed.Care(caringType);
        }

        [Button]
        public void DestroySeedbed()
        {
            OnDestroyed?.Invoke(this);
            Destroy(gameObject);
        }

        void IStartGameListener.OnStart()
        {
            _isEnable = true;
            _seedbed.Resume();
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

        void IFinishGameListener.OnFinish()
        {
            _isEnable = false;
            _seedbed.Stop();
        }

        void IExitGameListener.OnExit()
        {
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            _seedbed.OnStateChanged -= OnStateChanged;
            _seedbed.OnHarvestStateChanged -= OnHarvestStateChanged;
            _seedbed.OnCaringChanged -= OnCaringChanged;
        }

        private void OnStateChanged(SeedbedState state)
        {
            Debug.Log($"Seedbed changed state to {state}");
        }

        private void OnHarvestStateChanged(HarvestState state)
        {
            HarvestState = state;
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