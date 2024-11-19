using System;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Modules.Gardening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Gardening
{
    public class Seedbed :
        MonoBehaviour,
        IStartGameListener,
        IPauseGameListener,
        IResumeGameListener,
        IFinishGameListener,
        IExitGameListener, 
        IUpdateListener
    {
        public event Action<Plant, int> OnHarvestReceived;
        public event Action<Seedbed> OnDestroyed;

        private readonly ISeedbed _seedbed = new SeedbedImpl();
        private bool _isEnable;
        private int _count;
        [CanBeNull] public PlantConfig CurrentSeedConfig { get; private set; }

        [ShowInInspector, ReadOnly]
        public SeedbedState SeedbedState => _seedbed.State;
        
        [ShowInInspector, ReadOnly]
        public HarvestState HarvestState { get; private set; } = HarvestState.NotReady;

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

        public void Prepare()
        {
            if (!_isEnable) return;
            
            bool result = _seedbed.Prepare();
            Debug.Log($"Prepare seedbed: {result}");
        }

        public bool Seed(PlantConfig plantConfig, int count)
        {
            if (!_isEnable) return false;

            if (plantConfig is null) return false; 
            
            bool result = _seedbed.Seed(plantConfig);
            Debug.Log($"Seedbed seeded: {result}");

            if (!result) return false;
            
            _count = count;
            CurrentSeedConfig = plantConfig;

            return true;
        }

        public void Gather()
        {
            if (!_isEnable) return;
            
            bool gathered = _seedbed.Gather(out HarvestResult harvestResult);
            Debug.Log($"Seedbed gathered: {gathered}.");
            if (!gathered) return;
            
            Debug.Log($"HarvestResult: {harvestResult.IsCollected}, " +
                      $"{harvestResult.Value}, {harvestResult.Plant.PlantName}");
            
            if (!harvestResult.IsCollected) return;
            
            OnHarvestReceived?.Invoke(harvestResult.Plant, harvestResult.Value * _count);
            _count = 0;
            CurrentSeedConfig = null;
        }

        public void Care(Caring caringType)
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

        void IUpdateListener.OnUpdate(float deltaTime)
        {
            if (!_isEnable) return;
            
            _seedbed.Tick(deltaTime);
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
                Debug.Log($"Lost by reason {_seedbed.LostReason.CaringName}");
            }
        }

        private void OnCaringChanged(Caring type, CaringState caringState)
        {
            Debug.Log($"Care {type.CaringName} state changed to {caringState}!");
        }
    }
}