using System;
using Modules.GameCycle.Interfaces;
using Modules.Gardening;
using Sirenix.OdinInspector;
using Tavern.Gardening.Medicine;
using UnityEngine;

namespace Tavern.Gardening
{
    public class Seedbed :
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

        public ISeedbed SeedbedImpl { get; } = new SeedbedImpl();

        [ShowInInspector, ReadOnly] 
        private float _progress;

        [ShowInInspector, ReadOnly] 
        private float _dryingTimerProgress;

        [ShowInInspector, ReadOnly]
        private bool IsSick => SeedbedImpl.Harvest?.IsSick ?? false;

        [ShowInInspector, ReadOnly]
        private int SickProbability => SeedbedImpl.Harvest?.SickProbability ?? -1;

        private void Awake()
        {
            _isEnable = true;
        }

        private void OnEnable()
        {
            SeedbedImpl.OnHarvestProgressChanged += OnHarvestProgressChanged;
            SeedbedImpl.OnDryingTimerProgressChanged += OnDryingTimerChanged;
        }

        private void OnDisable()
        {
            SeedbedImpl.OnHarvestProgressChanged -= OnHarvestProgressChanged;
            SeedbedImpl.OnDryingTimerProgressChanged -= OnDryingTimerChanged;
        }

        public bool Seed(PlantConfig plantConfig)
        {
            if (!_isEnable || plantConfig is null) return false;

            bool result = SeedbedImpl.Seed(plantConfig);
            Debug.Log($"Seedbed seeded: {result}");

            return result;
        }

        public void Gather()
        {
            if (!_isEnable) return;
            
            bool gathered = SeedbedImpl.Gather(out HarvestResult harvestResult);
            Debug.Log($"Seedbed gathered: {gathered}.");
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
            
            SeedbedImpl.Watering();
        }

        public void Heal(int reducing)
        {
            if (!_isEnable) return;
            
            SeedbedImpl.Heal(reducing);
        }

        void IUpdateListener.OnUpdate(float deltaTime)
        {
            if (!_isEnable) return;

            SeedbedImpl.Tick(deltaTime);
        }

        void IStartGameListener.OnStart()
        {
            _isEnable = true;
            SeedbedImpl.Resume();
        }

        void IPauseGameListener.OnPause()
        {
            _isEnable = false;
            SeedbedImpl.Pause();
        }

        void IResumeGameListener.OnResume()
        {
            _isEnable = true;
            SeedbedImpl.Resume();
        }

        void IFinishGameListener.OnFinish()
        {
            _isEnable = false;
            SeedbedImpl.Stop();
        }

        private void OnHarvestProgressChanged(float progress) => _progress = progress;

        private void OnDryingTimerChanged(float progress) => _dryingTimerProgress = progress;
    }
}