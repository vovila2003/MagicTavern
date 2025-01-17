namespace Tavern.UI.Presenters
{
    public class CookingPanelPresenter
    {
        private const string Title = "Готовка";
        
        private readonly IPanelView _view;
        private readonly PresentersFactory _presentersFactory;
        
        private bool _isShown;
        private LeftGridRecipesPresenter _leftGridRecipesPresenter;
        private CookingMiniGamePresenter _cookingMiniGamePresenter;
        private CookingIngredientsPresenter _cookingIngredientsPresenter;

        public CookingPanelPresenter(IPanelView view, PresentersFactory presentersFactory)
        {
            _view = view;
            _presentersFactory = presentersFactory;
        }

        public void Show()
        {
            if (_isShown) return;
            
            SetupView();
            SetupLeftPanel();
            SetupMiniGame();
            SetupIngredients();

            _isShown = true;
        }

        private void Hide()
        {
            if (!_isShown) return;
            
            _view.OnCloseClicked -= Hide;
            
            _view.Hide();
            _leftGridRecipesPresenter.Hide();
            _cookingMiniGamePresenter.Hide();
            _cookingIngredientsPresenter.Hide();
            
            _isShown = false;
        }

        private void SetupView()
        {
            _view.SetTitle(Title);
            _view.OnCloseClicked += Hide;
            _view.Show();
        }

        private void SetupLeftPanel()
        {
            _leftGridRecipesPresenter ??= _presentersFactory.CreateLeftGridPresenter(_view.Container);
            _leftGridRecipesPresenter.Show();
        }

        private void SetupMiniGame()
        {
            _cookingMiniGamePresenter ??= _presentersFactory.CreateCookingMiniGamePresenter(_view.Container);
            _cookingMiniGamePresenter.Show();            
        }

        private void SetupIngredients()
        {
            _cookingIngredientsPresenter ??= _presentersFactory.CreateCookingIngredientsPresenter(_view.Container);
            _cookingIngredientsPresenter.Show();
        }
    }
}