using System;
using Modules.GameCycle.Interfaces;
using Modules.Gardening;
using Sirenix.OdinInspector;
using Tavern.Gardening.Medicine;
using UnityEngine;

namespace Tavern.Gardening
{
    public class Pot :
        MonoBehaviour,
        IStartGameListener,
        IPauseGameListener,
        IResumeGameListener,
        IFinishGameListener,
        IUpdateListener
    {
        public event Action<Plant, int> OnHarvestReceived;
        public event Action<int> OnSlopsReceived;

        private bool _isEnable;

        public ISeedbed Seedbed { get; } = new Seedbed();
        
        [ShowInInspector, ReadOnly] 
        private float _progress;

        [ShowInInspector, ReadOnly] 
        private float _dryingTimerProgress;

        [ShowInInspector, ReadOnly]
        public bool IsSick => Seedbed.Harvest?.IsSick ?? false;

        [ShowInInspector, ReadOnly]
        private int SickProbability => Seedbed.Harvest?.SickProbability ?? -1;
        
        public bool IsSeeded {get; private set;}
        public bool IsFertilized => Seedbed.IsFertilized;

        private void Awake()
        {
            _isEnable = true;
        }

        private void OnEnable()
        {
            Seedbed.OnHarvestProgressChanged += OnHarvestProgressChanged;
            Seedbed.OnDryingTimerProgressChanged += OnDryingTimerChanged;
        }

        private void OnDisable()
        {
            Seedbed.OnHarvestProgressChanged -= OnHarvestProgressChanged;
            Seedbed.OnDryingTimerProgressChanged -= OnDryingTimerChanged;
        }

        public bool Seed(PlantConfig plantConfig)
        {
            if (!_isEnable || plantConfig is null) return false;

            IsSeeded = Seedbed.Seed(plantConfig);
            Debug.Log($"Seedbed seeded: {IsSeeded}");

            return IsSeeded;
        }

        public void Gather()
        {
            if (!_isEnable) return;
            
            bool gathered = Seedbed.Gather(out HarvestResult harvestResult);
            Debug.Log($"Seedbed gathered: {gathered}.");
            IsSeeded = !gathered;
            if (!gathered) return;

            if (harvestResult.IsNormal)
            {
                OnHarvestReceived?.Invoke(harvestResult.Plant, harvestResult.Value);
            }
            else
            {
                OnSlopsReceived?.Invoke(harvestResult.Value);
            }
        }

        public void Watering()
        {
            if (!_isEnable) return;
            
            Seedbed.Watering();
        }

        public void Heal()
        {
            if (!_isEnable) return;
            
            Seedbed.Heal();
        }

        public void ReduceHarvestSicknessProbability(int reducing)
        {
            if (!_isEnable) return;
            
            Seedbed.ReduceHarvestSicknessProbability(reducing);
        }

        void IUpdateListener.OnUpdate(float deltaTime)
        {
            if (!_isEnable) return;

            Seedbed.Tick(deltaTime);
        }

        void IStartGameListener.OnStart()
        {
            _isEnable = true;
            Seedbed.Resume();
        }

        void IPauseGameListener.OnPause()
        {
            _isEnable = false;
            Seedbed.Pause();
        }

        void IResumeGameListener.OnResume()
        {
            _isEnable = true;
            Seedbed.Resume();
        }

        void IFinishGameListener.OnFinish()
        {
            _isEnable = false;
            Seedbed.Stop();
        }

        private void OnHarvestProgressChanged(float progress) => _progress = progress;

        private void OnDryingTimerChanged(float progress) => _dryingTimerProgress = progress;
    }
}