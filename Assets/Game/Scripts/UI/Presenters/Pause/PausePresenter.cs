using Modules.GameCycle;

namespace Tavern.UI.Presenters
{
    public sealed class PausePresenter : BasePresenter
    {
        private readonly IPauseView _view;
        private readonly GameCycle _gameCycle;
        private readonly IUiManager _uiManager;

        public PausePresenter(
            IPauseView view, 
            GameCycle gameCycle, 
            IUiManager uiManager) : base(view)
        {
            _view = view;
            _gameCycle = gameCycle;
            _uiManager = uiManager;
        }

        protected override void OnShow()
        {
            _view.OnResume += OnResume;
            _view.OnExit += OnExit;
        }

        protected override void OnHide()
        {
            _view.OnResume -= OnResume;
            _view.OnExit -= OnExit;
        }

        private void OnResume()
        {
            _uiManager.HidePause();
            _gameCycle.ResumeGame();            
        }

        private void OnExit()
        {
            _gameCycle.ExitGame();
        }
    }
}