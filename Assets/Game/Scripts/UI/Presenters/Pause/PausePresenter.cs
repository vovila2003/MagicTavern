using Tavern.Infrastructure;

namespace Tavern.UI.Presenters
{
    public sealed class PausePresenter : BasePresenter
    {
        private readonly IPauseView _view;
        private readonly GameCycleController _gameCycleController;
        private readonly UiManager _uiManager;

        public PausePresenter(
            IPauseView view, 
            GameCycleController gameCycleController, 
            UiManager uiManager) : base(view)
        {
            _view = view;
            _gameCycleController = gameCycleController;
            _uiManager = uiManager;
        }

        protected override void OnShow()
        {
            _view.OnResume += OnResume;
        }

        protected override void OnHide()
        {
            _view.OnResume -= OnResume;
        }

        private void OnResume()
        {
            _uiManager.HidePause();
            _gameCycleController.ResumeGame();            
        }
    }
}