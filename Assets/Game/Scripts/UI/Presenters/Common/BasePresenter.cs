namespace Tavern.UI.Presenters
{
    public abstract class BasePresenter
    {
        private readonly IView _view;

        protected BasePresenter(IView view)
        {
            _view = view;
        }

        private bool _isShown;

        public void Show()
        {
            if (_isShown) return;

            OnShow();
            _view.Show();
            _isShown = true;
        }

        public void Hide()
        {
            if (!_isShown) return;

            OnHide();
            _view.Hide();
            _isShown = false;
        }

        protected abstract void OnShow();
        protected abstract void OnHide();
    }
}