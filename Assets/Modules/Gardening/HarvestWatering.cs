using System;
using Modules.Timers;

namespace Modules.Gardening
{
    public class HarvestWatering
    {
        public event Action OnLost;
        public event Action OnWateringRequired;
        public event Action<float> OnDryingTimerProgressChanged;
        
        private readonly Timer _wateringTimer = new();
        private readonly Timer _dryingTimer = new();

        public HarvestWatering(Plant plant)
        {
            SetupWateringTimer(plant);
            SetupDryingTimer(plant);
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

        private void SetupWateringTimer(Plant plant)
        {
            _wateringTimer.Loop = true;
            _wateringTimer.Duration = plant.GrowthDuration / plant.WateringAmount;
            _wateringTimer.OnEnded += OnWateringNeeded;
        }

        private void SetupDryingTimer(Plant plant)
        {
            _dryingTimer.Loop = false;
            _dryingTimer.Duration = plant.GrowthDuration / plant.WateringAmount;
            _dryingTimer.OnEnded += OnDry;
            _dryingTimer.OnProgressChanged += OnProgressChanged;
        }

        private void OnWateringNeeded()
        {
            OnWateringRequired?.Invoke();
            _dryingTimer.Start();
        }

        private void OnDry() => OnLost?.Invoke();

        private void OnProgressChanged(float progress) => OnDryingTimerProgressChanged?.Invoke(progress);
    }
}