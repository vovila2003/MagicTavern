using System;
using Modules.Gardening;
using Sirenix.OdinInspector;
using Tavern.Common;
using UnityEngine;

namespace Tavern.Gardening
{
    public class Pot : MonoBehaviour
    {
        public event Action OnActivated;
        public event Action<PlantConfig, int, bool> OnHarvestReceived;
        public event Action<int> OnSlopsReceived;

        [SerializeField] 
        private Interactor Interactor;

        [SerializeField] 
        private HarvestView View;

        private bool _isEnable;

        public ISeedbed Seedbed { get; } = new Seedbed();
        
        [ShowInInspector, ReadOnly] 
        public float Progress { get; private set; }

        [ShowInInspector, ReadOnly] 
        private float _dryingTimerProgress;

        [ShowInInspector, ReadOnly]
        public bool IsSick => Seedbed.Harvest?.IsSick ?? false;

        [ShowInInspector, ReadOnly]
        private int SickProbability => Seedbed.Harvest?.SickProbability ?? -1;
        
        public bool IsSeeded {get; private set;}
        public bool IsFertilized => Seedbed.IsFertilized;

        public Sprite CurrentSprite => View.CurrentSprite;
        public SeedItemConfig CurrentSeedConfig { get; private set; }

        public bool WaterRequired => Seedbed.Harvest.WaterRequired;

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

        public void Setup()
        {
            Interactor.OnActivated += OnAction;
        }

        public bool Seed(SeedItemConfig seedConfig)
        {
            if (!_isEnable || seedConfig is null) return false;

            if (!seedConfig.TryGet(out ComponentPlant componentPlant))
            {
                return false;
            }

            PlantConfig plantConfig = componentPlant.Config;
            if (plantConfig is null)
            {
                Debug.Log($"PlantConfig in {seedConfig.Name} is null");
                return false;
            }

            IsSeeded = Seedbed.Seed(plantConfig);
            Progress = 0;
            Debug.Log($"Seedbed seeded: {IsSeeded}");
            if (IsSeeded)
            {
                CurrentSeedConfig = seedConfig;
            }

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
                OnHarvestReceived?.Invoke(harvestResult.PlantConfig, 
                    harvestResult.Value, harvestResult.HasSeedInHarvest);
            }
            else
            {
                OnSlopsReceived?.Invoke(harvestResult.Value);
            }

            CurrentSeedConfig = null;
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

        public void Tick(float deltaTime) => Seedbed.Tick(deltaTime);

        public void OnStart() => Seedbed.Resume();

        public void OnPause() => Seedbed.Pause();

        public void OnResume() => Seedbed.Resume();

        public void OnFinish() => Seedbed.Stop();

        private void OnHarvestProgressChanged(float progress) => Progress = progress;

        private void OnDryingTimerChanged(float progress) => _dryingTimerProgress = progress;

        private void OnAction() => OnActivated?.Invoke();
    }
}