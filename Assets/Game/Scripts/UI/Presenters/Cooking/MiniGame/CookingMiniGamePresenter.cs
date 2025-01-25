using Tavern.Cooking;
using Tavern.Cooking.MiniGame;

namespace Tavern.UI.Presenters
{
    public class CookingMiniGamePresenter : BasePresenter
    {
        private const string Start = "Старт";
        private const string Stop = "Стоп";
        
        private readonly ICookingMiniGameView _view;
        private readonly DishCrafter _crafter;
        private readonly MiniGamePlayer _player;

        public CookingMiniGamePresenter(
            ICookingMiniGameView view,
            DishCrafter crafter,
            MiniGamePlayer player) : base(view)
        {
            _view = view;
            _crafter = crafter;
            _player = player;
        }
        
        protected override void OnShow()
        {
            SetupView();
            _crafter.OnStateChanged += OnCraftStateChanged;
            _view.OnButtonClicked += OnButtonClicked;
            _player.OnGameStarted += OnGameStarted;
            _player.OnGameStopped += OnGameStopped;
        }

        protected override void OnHide()
        {
            _crafter.OnStateChanged -= OnCraftStateChanged;
            _view.OnButtonClicked -= OnButtonClicked;
            _player.OnGameStarted -= OnGameStarted;
            _player.OnGameStopped -= OnGameStopped;
        }

        private void SetupView()
        {
            _view.SetStartButtonActive(false);
            _view.SetButtonText(Start);
        }

        private void OnCraftStateChanged(bool state)
        {
            _view.SetStartButtonActive(state);
            if (state)
            {
                SetRegions(_player.GetRegions());
            }
        }

        private void SetRegions(Regions regions)
        {
            _view.SetYellowZone(regions.RedYellow, regions.YellowRed);
            _view.SetGreenZone(regions.YellowGreen, regions.GreenYellow);
        }

        private void OnButtonClicked()
        {
            _player.Activate();
        }

        private void OnGameStarted()
        {
            _view.SetButtonText(Stop);
            _player.OnGameValueChanged += OnValueChanged;
        }

        private void OnGameStopped()
        {
            _view.SetButtonText(Start);
            _player.OnGameValueChanged -= OnValueChanged;
        }

        private void OnValueChanged(float value)
        {
            _view.SetSliderValue(value);
        }
    }
}