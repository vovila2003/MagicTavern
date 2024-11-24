using System;
using Modules.Timers;

namespace Modules.Gardening
{
    public class HarvestWatering
    {
        private const float PenaltyDryingTimerDuration = 0.5f;
        
        private readonly Harvest _harvest;
        public event Action OnLost;
        public event Action OnWateringRequired;
        public event Action<float> OnDryingTimerProgressChanged;
        
        private readonly Timer _wateringTimer = new();
        private readonly Timer _dryingTimer = new();
        private float _baseDryingTimerDuration;

        public HarvestWatering(Harvest harvest, Plant plant, int acceleration)
        {
            _harvest = harvest;
            SetupWateringTimer(plant, acceleration);
            SetupDryingTimer(plant, acceleration);
        }

        public void Start() => _wateringTimer.Start();

        public void Stop()
        {
            _wateringTimer.Stop();
            _dryingTimer.Stop();
        }

        public void Water() => _dryingTimer.Stop();

        public void Tick(float deltaTime)
        {
            _wateringTimer.Tick(deltaTime);
            _dryingTimer.Tick(deltaTime);    
        }

        public void Dispose()
        {
            _wateringTimer.OnEnded -= OnWateringNeeded;
            _dryingTimer.OnEnded -= OnDry;
            _dryingTimer.OnProgressChanged -= OnProgressChanged;
        }

        private void SetupWateringTimer(Plant plant, int acceleration)
        {
            _wateringTimer.Loop = true;
            _wateringTimer.Duration = plant.GrowthDuration * (1 - acceleration / 100.0f) / plant.WateringAmount;
            _wateringTimer.OnEnded += OnWateringNeeded;
        }

        private void SetupDryingTimer(Plant plant, int acceleration)
        {
            _dryingTimer.Loop = false;
            _baseDryingTimerDuration = plant.GrowthDuration * (1 - acceleration / 100.0f) / plant.WateringAmount; 
            _dryingTimer.Duration = _baseDryingTimerDuration;
            _dryingTimer.OnEnded += OnDry;
            _dryingTimer.OnProgressChanged += OnProgressChanged;
        }

        private void OnWateringNeeded()
        {
            OnWateringRequired?.Invoke();
            _dryingTimer.Duration = _baseDryingTimerDuration * (_harvest.IsSick ? PenaltyDryingTimerDuration : 1f);
            _dryingTimer.Start();
        }

        private void OnDry() => OnLost?.Invoke();

        private void OnProgressChanged(float progress) => OnDryingTimerProgressChanged?.Invoke(progress);
    }
}