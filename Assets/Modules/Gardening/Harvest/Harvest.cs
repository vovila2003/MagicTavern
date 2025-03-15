using System;
using Modules.Timers;

namespace Modules.Gardening
{
    public class Harvest : IHarvest  
    {
        private const float TimePartToPenalty = 0.5f;
        public event Action<HarvestState> OnStateChanged;
        public event Action<HarvestAge> OnAgeChanged;
        public event Action OnWaterRequired;
        public event Action<float> OnProgressChanged;
        public event Action<float> OnDryingTimerProgressChanged;
        public event Action OnSick;

        public int Value { get; set; }
        public HarvestState State { get; set; }
        public HarvestAge Age { get; set; }
        public PlantConfig PlantConfig { get; }
        public bool IsSick { get; set; }
        public int SickProbability => HarvestSickness.Probability;
        public bool IsWaterRequired { get; set; }
        public bool IsPaused { get; set; }
        public bool IsReadyAfterWatering { get; set; }
        public bool IsPenalized { get; set; }
        public int ResultHarvestAmount { get; set; }
        public Timer GrowthTimer { get; } = new();
        public HarvestWatering HarvestWatering { get; private set; }
        public HarvestSickness HarvestSickness { get; private set; }

        public Harvest(PlantConfig plantConfig)
        {
            Value = 0;
            PlantConfig = plantConfig;
            ResultHarvestAmount = plantConfig.Plant.ResultValue;

            SetupGrowthTimer(plantConfig.Plant);
            SetupWatering(plantConfig.Plant);
            SetupSickness(plantConfig.Plant);
        }

        public void StartGrow()
        {
            State = HarvestState.Growing;
            Age = HarvestAge.Baby;
            IsWaterRequired = false;
            IsPaused = false;
            
            OnStateChanged?.Invoke(State);
            OnAgeChanged?.Invoke(Age);

            HarvestWatering.Start();
            GrowthTimer.Start();
        }

        public void StopGrow()
        {
            HarvestWatering.Stop();
            GrowthTimer.Stop();
            Dispose();
        }

        public void Watering()
        {
            HarvestWatering.Water();
            IsWaterRequired = false;
            IsPenalized = false;

            if (IsReadyAfterWatering)
            {
                ReadyHarvest();
                return;
            }
            
            if (!IsPaused) return;
            
            GrowthTimer.Resume();
            IsPaused = false;
        }

        public void Heal()
        {
            IsSick = false;
        }

        public void ReduceHarvestSicknessProbability(int reducing)
        {
            HarvestSickness.DecreaseSicknessProbability(reducing);
        }

        public void BoostHarvestAmount(int boostInPercent)
        {
            ResultHarvestAmount  = (int)(ResultHarvestAmount * (1 + boostInPercent / 100.0f));
        }

        public void AccelerateGrowth(int accelerationInPercent)
        {
            float currentDuration = GrowthTimer.Duration;
            float newDuration = currentDuration * (1 - accelerationInPercent / 100.0f);
            GrowthTimer.Duration = newDuration;
            
            HarvestWatering.SetNewDuration(accelerationInPercent);
        }

        public void Tick(float deltaTime)
        {
            //Order is important
            HarvestWatering.Tick(deltaTime);
            GrowthTimer.Tick(deltaTime);
        }

        public void SetIsWaterRequired(bool isWaterRequired)
        {
            IsWaterRequired = isWaterRequired;
            if (IsWaterRequired)
            {
                OnWaterRequired?.Invoke();
            }
        }

        public void SetState(HarvestState state)
        {
            State = state;
            OnStateChanged?.Invoke(State);
        }

        public void SetAge(HarvestAge age)
        {
            Age = age;
            
            OnAgeChanged?.Invoke(Age);
        }

        private void SetupGrowthTimer(Plant plant)
        {
            GrowthTimer.Loop = false;
            GrowthTimer.Duration = plant.GrowthDuration;
            GrowthTimer.OnEnded += OnGrowthEnded;
            GrowthTimer.OnProgressChanged += OnGrowthProgressChanged;
        }

        public void SetupWatering(Plant plant)
        {
            HarvestWatering = new HarvestWatering(this, plant);
            HarvestWatering.OnWateringRequired += OnWateringRequired;
            HarvestWatering.OnLost += OnHarvestDry;
            HarvestWatering.OnDryingTimerProgressChanged += OnDryingProgressChanged;
        }

        public void SetupSickness(Plant plant)
        {
            HarvestSickness = new HarvestSickness(plant);
            OnAgeChanged += CheckSickness;
        }

        private void CheckSickness(HarvestAge age)
        {
            if (age == HarvestAge.Old) return;

            bool isSick = HarvestSickness.IsSick();
            IsSick = IsSick || isSick;
            if (IsSick)
            {
                OnSick?.Invoke();
            }
        }

        private void OnHarvestDry()
        {
            Value = ResultHarvestAmount;
            State = HarvestState.Dried;
            OnStateChanged?.Invoke(State);
            StopGrow();
        }

        private void OnWateringRequired()
        {
            OnWaterRequired?.Invoke();
            IsWaterRequired = true;
        }

        private void OnGrowthEnded()
        {
            if (IsWaterRequired)
            {
                IsReadyAfterWatering = true;
                return;
            }

            ReadyHarvest();
        }

        private void ReadyHarvest()
        {
            Value = ResultHarvestAmount;
            
            State = IsSick? HarvestState.Lost : HarvestState.Ready;
            OnStateChanged?.Invoke(State);
            if (Age != HarvestAge.Old)
            {
                Age = HarvestAge.Old;
                OnAgeChanged?.Invoke(Age);
            }
            
            StopGrow();
        }

        private void OnGrowthProgressChanged(float progress)
        {
            OnProgressChanged?.Invoke(progress);
            
            HarvestAge newAge = progress switch
            {
                < 0.5f => HarvestAge.Baby,
                >= 0.5f and < 1f => HarvestAge.Teen,
                >= 1 => HarvestAge.Old,
                _ => throw new ArgumentOutOfRangeException(nameof(progress), progress, null)
            };

            if (Age == newAge) return;

            TransitionToNextAge(newAge);
        }

        private void TransitionToNextAge(HarvestAge newAge)
        {
            if (IsWaterRequired)
            {
                PauseGrowth();
                return;
            }
            
            Age = newAge;
            OnAgeChanged?.Invoke(Age);
        }

        private void PauseGrowth()
        {
            GrowthTimer.Pause();
            IsPaused = true;
        }

        private void Dispose()
        {
            GrowthTimer.OnEnded -= OnGrowthEnded;
            GrowthTimer.OnProgressChanged -= OnGrowthProgressChanged;
            
            HarvestWatering.OnWateringRequired -= OnWateringRequired;
            HarvestWatering.OnLost -= OnHarvestDry;
            HarvestWatering.OnDryingTimerProgressChanged -= OnDryingProgressChanged;
            
            HarvestWatering.Dispose();
            
            OnAgeChanged -= CheckSickness;
        }

        private void OnDryingProgressChanged(float progress)
        {
            OnDryingTimerProgressChanged?.Invoke(progress);

            if (IsPenalized) return;

            if (progress < TimePartToPenalty) return;
            
            IsPenalized = true;
            HarvestSickness.Penalty();
        }
    }
}