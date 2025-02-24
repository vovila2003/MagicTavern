namespace Tavern.UI.Presenters
{
    public abstract class BasePresenter
    {
        private readonly IView _view;

        protected BasePresenter(IView view)
        {
            _view = view;
        }

        public bool IsShown { get; private set; }

        public void Show()
        {
            if (IsShown) return;

            OnShow();
            _view.Show();
            IsShown = true;
        }

        public void Hide()
        {
            if (!IsShown) return;

            OnHide();
            _view.Hide();
            IsShown = false;
        }

        protected abstract void OnShow();
        protected abstract void OnHide();
    }
}