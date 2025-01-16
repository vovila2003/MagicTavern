namespace Tavern.UI.Presenters
{
    public class CookingPanelPresenter
    {
        private readonly ICookingView _view;
        private readonly PresentersFactory _presentersFactory;
        
        private bool _isShown;
        private LeftGridRecipesPresenter _leftGridRecipesPresenter;

        public CookingPanelPresenter(ICookingView view, PresentersFactory presentersFactory)
        {
            _view = view;
            _presentersFactory = presentersFactory;
        }

        public void Show()
        {
            if (_isShown) return;

            SetupLeftPanel();
            _view.OnCloseClicked += Hide;
            _view.Show();
            
            _isShown = true;
        }

        private void Hide()
        {
            if (!_isShown) return;
            
            _view.OnCloseClicked -= Hide;
            
            _view.Hide();
            _leftGridRecipesPresenter.Hide();
            
            _isShown = false;
        }

        private void SetupLeftPanel()
        {
            _leftGridRecipesPresenter ??= _presentersFactory.CreateLeftGridPresenter(_view.Container);

            _leftGridRecipesPresenter.Show();
        }
    }
}