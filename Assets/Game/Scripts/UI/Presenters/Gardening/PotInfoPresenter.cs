using System;
using Modules.Gardening;
using Tavern.Components;
using Tavern.Gardening;
using Tavern.Settings;

namespace Tavern.UI.Presenters
{
    public class PotInfoPresenter : BasePresenter
    {
        public event Action OnGather;
        
        private readonly IPotInfoView _view;
        private readonly GardeningUISettings _settings;
        private readonly Seeder _seeder;
        private Pot _pot;

        public PotInfoPresenter(
            IPotInfoView view,
            GardeningUISettings settings,
            Seeder seeder
        ) : base(view)
        {
            _view = view;
            _settings = settings;
            _seeder = seeder;
        }

        public void Show(Pot pot)
        {
            _pot = pot;
            Show();
        }
        
        protected override void OnShow()
        {
            if (_pot.IsSeeded)
            {
                SetupSeededPot();
            }
            else
            {
                SetupEmptyPot();
            }

            _view.OnGatherClicked += OnGatherClicked;
            _view.OnWateringClicked += OnWaterClicked;
        }

        protected override void OnHide()
        {
            _view.OnGatherClicked -= OnGatherClicked;
            _view.OnWateringClicked -= OnWaterClicked;
        }

        private void SetupSeededPot()
        {
            _view.SetTitle(_pot.CurrentSeedConfig.Metadata.Title);
            _view.SetIcon(_pot.CurrentSprite);
            _view.SetProgress(_pot.Progress);
            _view.SetIsFertilized(_pot.IsFertilized);
            _view.SetSickProbability(_pot.Seedbed.Harvest.SickProbability);
            bool isReady = _pot.Seedbed.Harvest.State != HarvestState.Growing;
            if (isReady)
            {
                _view.SetIsSick(false);
                _view.SetIsWaterNeed(false);
                _view.SetWateringActive(false);
            }
            else
            {
                _view.SetIsSick(_pot.IsSick);
                bool waterRequired = _pot.WaterRequired;
                _view.SetIsWaterNeed(waterRequired);
                _view.SetWateringActive(waterRequired);
            }
            
            _view.SetGatherActive(isReady);
        }

        private void SetupEmptyPot()
        {
            _view.SetTitle(string.Empty);
            _view.SetIcon(_settings.EmptyPotSprite);
            _view.SetProgress(0);
            _view.SetIsFertilized(false);
            _view.SetIsSick(false);
            _view.SetIsWaterNeed(false);
            _view.SetWateringActive(false);
            _view.SetGatherActive(false);
            _view.SetSickProbability(0);
        }

        private void OnGatherClicked()
        {
            bool result = _seeder.Gather(_pot);
            if (!result) return;
            
            OnGather?.Invoke();
        }

        private void OnWaterClicked()
        {
            bool result = _seeder.Watering(_pot);
            if (!result) return;
            
            _view.SetIsWaterNeed(false);
            _view.SetWateringActive(false);
        }
    }
}