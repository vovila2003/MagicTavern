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

        private readonly int _resultHarvestAmount;
        private readonly Timer _growthTimer = new();
        private HarvestWatering _watering;
        private bool _waterRequired;
        private bool _isPaused;
        private bool _readyAfterWatering;

        private HarvestSickness _harvestSickness;
        private bool _isPenalized;

        public int Value { get; private set; }
        public HarvestState State { get; private set; }
        public HarvestAge Age { get; private set; }
        public PlantConfig PlantConfig { get; }
        public bool IsSick { get; private set; }
        public int SickProbability => _harvestSickness.Probability;

        public Harvest(PlantConfig plantConfig)
        {
            Value = 0;
            PlantConfig = plantConfig;
            _resultHarvestAmount = plantConfig.Plant.ResultValue;

            SetupGrowthTimer(plantConfig.Plant);
            SetupWatering(plantConfig.Plant);
            SetupSickness(plantConfig.Plant);
        }

        public void StartGrow()
        {
            State = HarvestState.Growing;
            Age = HarvestAge.Baby;
            _waterRequired = false;
            _isPaused = false;
            
            OnStateChanged?.Invoke(State);
            OnAgeChanged?.Invoke(Age);

            _watering.Start();
            _growthTimer.Start();
        }

        public void StopGrow()
        {
            _watering.Stop();
            _growthTimer.Stop();
            Dispose();
        }

        public void Watering()
        {
            _watering.Water();
            _waterRequired = false;
            _isPenalized = false;

            if (_readyAfterWatering)
            {
                ReadyHarvest();
                return;
            }
            
            if (!_isPaused) return;
            
            _growthTimer.Resume();
            _isPaused = false;
        }

        public void Heal(int medicineReducing)
        {
            IsSick = false;
            _harvestSickness.DecreaseSicknessProbability(medicineReducing);
        }

        public void Tick(float deltaTime)
        {
            //Order is important
            _watering.Tick(deltaTime);
            _growthTimer.Tick(deltaTime);
        }

        private void SetupGrowthTimer(Plant plant)
        {
            _growthTimer.Loop = false;
            _growthTimer.Duration = plant.GrowthDuration;
            _growthTimer.OnEnded += OnGrowthEnded;
            _growthTimer.OnProgressChanged += OnGrowthProgressChanged;
        }

        private void SetupWatering(Plant plant)
        {
            _watering = new HarvestWatering(this, plant);
            _watering.OnWateringRequired += OnWateringRequired;
            _watering.OnLost += OnHarvestDry;
            _watering.OnDryingTimerProgressChanged += OnDryingProgressChanged;
        }

        private void SetupSickness(Plant plant)
        {
            _harvestSickness = new HarvestSickness(plant);
            OnAgeChanged += CheckSickness;
        }

        private void CheckSickness(HarvestAge age)
        {
            if (age == HarvestAge.Old) return;
            
            IsSick = IsSick || _harvestSickness.IsSick();
            if (IsSick)
            {
                OnSick?.Invoke();
            }
        }

        private void OnHarvestDry()
        {
            Value = _resultHarvestAmount;
            State = HarvestState.Dried;
            OnStateChanged?.Invoke(State);
            StopGrow();
        }

        private void OnWateringRequired()
        {
            OnWaterRequired?.Invoke();
            _waterRequired = true;
        }

        private void OnGrowthEnded()
        {
            if (_waterRequired)
            {
                _readyAfterWatering = true;
                return;
            }

            ReadyHarvest();
        }

        private void ReadyHarvest()
        {
            Value = _resultHarvestAmount;
            
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
            if (_waterRequired)
            {
                PauseGrowth();
                return;
            }
            
            Age = newAge;
            OnAgeChanged?.Invoke(Age);
        }

        private void PauseGrowth()
        {
            _growthTimer.Pause();
            _isPaused = true;
        }

        private void Dispose()
        {
            _growthTimer.OnEnded -= OnGrowthEnded;
            _growthTimer.OnProgressChanged -= OnGrowthProgressChanged;
            
            _watering.OnWateringRequired -= OnWateringRequired;
            _watering.OnLost -= OnHarvestDry;
            _watering.OnDryingTimerProgressChanged -= OnDryingProgressChanged;
            
            _watering.Dispose();
            
            OnAgeChanged -= CheckSickness;
        }

        private void OnDryingProgressChanged(float progress)
        {
            OnDryingTimerProgressChanged?.Invoke(progress);

            if (_isPenalized) return;

            if (progress < TimePartToPenalty) return;
            
            _isPenalized = true;
            _harvestSickness.Penalty();
        }
    }
}