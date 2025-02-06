using Tavern.Cooking.MiniGame;
using Tavern.InputServices.Interfaces;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class CookingMiniGamePresenter : BasePresenter
    {
        private const string Start = "Старт";
        private const string Stop = "Стоп";
        
        private readonly ICookingMiniGameView _view;

        private readonly MiniGamePlayer _player;

        private readonly ISpaceInput _inputService;
        private Sprite _sprite;

        public CookingMiniGamePresenter(
            ICookingMiniGameView view,
            MiniGamePlayer player,
            ISpaceInput input) : base(view)
        {
            _view = view;
            _player = player;
            _inputService = input;
        }

        public void Show(Sprite sprite)
        {
            _sprite = sprite;
            Show();
        }

        protected override void OnShow()
        {
            SetupView();
            _view.OnButtonClicked += OnButtonClicked;
            _player.OnGameStarted += OnGameStarted;
            _player.OnGameStopped += OnGameStopped;
            _player.OnGameAvailableChange += OnGameAvailableChanged;
            _player.OnTimeChanged += SetTimer;
            _inputService.OnSpace += OnButtonClicked;
        }

        protected override void OnHide()
        {
            _view.OnButtonClicked -= OnButtonClicked;
            _player.OnGameStarted -= OnGameStarted;
            _player.OnGameStopped -= OnGameStopped;
            _player.OnGameAvailableChange -= OnGameAvailableChanged;
            _player.OnTimeChanged -= SetTimer;
            _inputService.OnSpace -= OnButtonClicked;
            
            _player.StopGame();
        }

        private void SetupView()
        {
            _view.SetIcon(_sprite);
            _view.SetStartButtonActive(false);
            _view.SetButtonText(Start);
        }

        private void OnGameAvailableChanged(bool state)
        {
            _view.SetStartButtonActive(state);
            if (!state)
            {
                SetTimer(0);
                return;
            }
            
            GameParams gameParams = _player.GetGameParams();
            SetRegions(gameParams.Regions);
            SetTimer(gameParams.TimeInSeconds);
        }

        private void SetRegions(Regions regions)
        {
            _view.SetYellowZone(regions.RedYellow, regions.YellowRed);
            _view.SetGreenZone(regions.YellowGreen, regions.GreenYellow);
        }

        private void SetTimer(float timeInSeconds)
        {
            var minutes = (int)(timeInSeconds / 60);
            var remainingSeconds = (int)(timeInSeconds % 60);
            var milliseconds = (int)((timeInSeconds - (int)timeInSeconds) * 1000);

            _view.SetTimerText($"{minutes:D2}:{remainingSeconds:D2}:{milliseconds:D3}");
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