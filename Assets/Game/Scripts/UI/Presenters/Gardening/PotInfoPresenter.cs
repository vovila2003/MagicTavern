using Tavern.Gardening;
using Tavern.Settings;

namespace Tavern.UI.Presenters
{
    public class PotInfoPresenter : BasePresenter
    {
        private readonly IPotInfoView _view;
        private readonly GardeningUISettings _settings;
        private Pot _pot;

        public PotInfoPresenter(
            IPotInfoView view,
            GardeningUISettings settings
        ) : base(view)
        {
            _view = view;
            _settings = settings;
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
                return;
            }

            SetupEmptyPot();
        }

        protected override void OnHide()
        {
            
        }

        private void SetupSeededPot()
        {
            _view.SetTitle(_pot.CurrentSeedConfig.Metadata.Title);
            _view.SetIcon(_pot.CurrentSprite);
            _view.SetProgress(_pot.Progress);
            _view.SetIsFertilized(_pot.IsFertilized);
            _view.SetIsSick(_pot.IsSick);
            _view.SetIsWaterNeed(_pot.WaterRequired);
        }

        private void SetupEmptyPot()
        {
            _view.SetTitle(string.Empty);
            _view.SetIcon(_settings.EmptyPotSprite);
            _view.SetProgress(0);
            _view.SetIsFertilized(false);
            _view.SetIsSick(false);
            _view.SetIsWaterNeed(false);
        }
    }
}