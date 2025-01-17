namespace Tavern.UI.Presenters
{
    public class CookingIngredientsPresenter
    {
        private readonly IIngredientsView _view;
        private bool _isShown;

        public CookingIngredientsPresenter(IIngredientsView view)
        {
            _view = view;
        }

        public void Show()
        {
            if (_isShown) return;
            
            _view.Show();
            
            _isShown = true;
        }

        public void Hide()
        {
            if (!_isShown) return;
            
            _view.Hide();
            
            _isShown = false;
        }
    }
}