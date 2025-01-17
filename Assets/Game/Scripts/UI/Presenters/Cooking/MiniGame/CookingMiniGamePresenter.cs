namespace Tavern.UI.Presenters
{
    public class CookingMiniGamePresenter
    {
        private readonly ICookingMiniGameView _view;
        private bool _isShown;

        public CookingMiniGamePresenter(ICookingMiniGameView view)
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