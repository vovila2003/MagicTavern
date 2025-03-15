using System;
using Modules.Timers;

namespace Modules.Gardening
{
    public class HarvestWatering
    {
        private const float PenaltyDryingTimerDuration = 0.5f;
        
        public event Action OnLost;
        public event Action OnWateringRequired;
        public event Action<float> OnDryingTimerProgressChanged;
        
        private readonly Harvest _harvest;

        public float BaseDryingTimerDuration { get; set; }

        public Timer WateringTimer { get; } = new();

        public Timer DryingTimer { get; } = new();

        public HarvestWatering(Harvest harvest, Plant plant)
        {
            _harvest = harvest;
            SetupWateringTimer(plant);
            SetupDryingTimer(plant);
        }

        public void Start() => WateringTimer.Start();

        public void Stop()
        {
            WateringTimer.Stop();
            DryingTimer.Stop();
        }

        public void Water() => DryingTimer.Stop();

        public void SetNewDuration(int accelerationInPercent)
        {
            float currentWateringDuration = WateringTimer.Duration;
            float newWateringDuration = currentWateringDuration * (1 - accelerationInPercent / 100f );
            WateringTimer.Duration = newWateringDuration;
            BaseDryingTimerDuration = newWateringDuration;
        }

        public void Tick(float deltaTime)
        {
            WateringTimer.Tick(deltaTime);
            DryingTimer.Tick(deltaTime);    
        }

        public void Dispose()
        {
            WateringTimer.OnEnded -= OnWateringNeeded;
            DryingTimer.OnEnded -= OnDry;
            DryingTimer.OnProgressChanged -= OnProgressChanged;
        }

        private void SetupWateringTimer(Plant plant)
        {
            WateringTimer.Loop = true;
            WateringTimer.Duration = plant.GrowthDuration / plant.WateringAmount;
            WateringTimer.OnEnded += OnWateringNeeded;
        }

        private void SetupDryingTimer(Plant plant)
        {
            DryingTimer.Loop = false;
            BaseDryingTimerDuration = plant.GrowthDuration / plant.WateringAmount; 
            DryingTimer.Duration = BaseDryingTimerDuration;
            DryingTimer.OnEnded += OnDry;
            DryingTimer.OnProgressChanged += OnProgressChanged;
        }

        private void OnWateringNeeded()
        {
            OnWateringRequired?.Invoke();
            DryingTimer.Duration = BaseDryingTimerDuration * (_harvest.IsSick ? PenaltyDryingTimerDuration : 1f);
            DryingTimer.Start();
        }

        private void OnDry() => OnLost?.Invoke();

        private void OnProgressChanged(float progress) => OnDryingTimerProgressChanged?.Invoke(progress);
    }
}