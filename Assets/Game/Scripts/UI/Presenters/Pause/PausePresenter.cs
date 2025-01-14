using Tavern.Architecture;
using Tavern.UI.Views;

namespace Tavern.UI.Presenters
{
    public sealed class PausePresenter
    {
        private readonly PauseView _view;
        private readonly PauseGameController _pauseGameController;

        public PausePresenter(PauseView view, PauseGameController pauseGameController)
        {
            _view = view;
            _pauseGameController = pauseGameController;
        }

        public void Show()
        {
            _view.OnResume += OnResume;
            _view.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _view.OnResume -= OnResume;
            _view.gameObject.SetActive(false);
        }

        private void OnResume()
        {
            _pauseGameController.OnPauseResume();            
        }
    }
}