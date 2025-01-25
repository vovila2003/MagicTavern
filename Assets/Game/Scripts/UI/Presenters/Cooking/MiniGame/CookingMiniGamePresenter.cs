using Tavern.Cooking.MiniGame;
using Tavern.InputServices.Interfaces;

namespace Tavern.UI.Presenters
{
    public class CookingMiniGamePresenter : BasePresenter
    {
        private const string Start = "Старт";
        private const string Stop = "Стоп";
        
        private readonly ICookingMiniGameView _view;
        private readonly MiniGamePlayer _player;
        private readonly ISpaceInput _inputService;

        public CookingMiniGamePresenter(
            ICookingMiniGameView view,
            MiniGamePlayer player,
            ISpaceInput input) : base(view)
        {
            _view = view;
            _player = player;
            _inputService = input;
        }
        
        protected override void OnShow()
        {
            SetupView();
            _view.OnButtonClicked += OnButtonClicked;
            _player.OnGameStarted += OnGameStarted;
            _player.OnGameStopped += OnGameStopped;
            _player.OnGameAvailableChange += OnGameAvailableChanged;
            _inputService.OnSpace += OnButtonClicked;
        }

        protected override void OnHide()
        {
            _view.OnButtonClicked -= OnButtonClicked;
            _player.OnGameStarted -= OnGameStarted;
            _player.OnGameStopped -= OnGameStopped;
            _player.OnGameAvailableChange -= OnGameAvailableChanged;
            _inputService.OnSpace -= OnButtonClicked;
            _player.StopGame();
        }

        private void SetupView()
        {
            _view.SetStartButtonActive(false);
            _view.SetButtonText(Start);
        }

        private void OnGameAvailableChanged(bool state)
        {
            _view.SetStartButtonActive(state);
            _view.SetSliderValue(0);
            if (!state) return;
            
            SetRegions(_player.GetRegions());
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