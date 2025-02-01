using Tavern.Infrastructure;

namespace Tavern.UI.Presenters
{
    public sealed class PausePresenter : BasePresenter
    {
        private readonly IPauseView _view;
        private readonly PauseGameController _pauseGameController;

        public PausePresenter(IPauseView view, PauseGameController pauseGameController) : base(view)
        {
            _view = view;
            _pauseGameController = pauseGameController;
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
            _pauseGameController.OnPauseResume();            
        }
    }
}