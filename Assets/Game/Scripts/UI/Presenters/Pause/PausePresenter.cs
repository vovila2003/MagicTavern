using Tavern.Architecture;

namespace Tavern.UI.Presenters
{
    public sealed class PausePresenter
    {
        private readonly IPauseView _view;
        private readonly PauseGameController _pauseGameController;

        public PausePresenter(IPauseView view, PauseGameController pauseGameController)
        {
            _view = view;
            _pauseGameController = pauseGameController;
        }

        public void Show()
        {
            _view.OnResume += OnResume;
            _view.Show();
        }

        public void Hide()
        {
            _view.OnResume -= OnResume;
            _view.Hide();
        }

        private void OnResume()
        {
            _pauseGameController.OnPauseResume();            
        }
    }
}