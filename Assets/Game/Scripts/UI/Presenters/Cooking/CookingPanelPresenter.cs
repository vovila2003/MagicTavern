namespace Tavern.UI.Presenters
{
    public class CookingPanelPresenter : BasePresenter
    {
        private const string Title = "Готовка";
        
        private readonly IPanelView _view;
        private readonly PresentersFactory _presentersFactory;
        
        private LeftGridRecipesPresenter _leftGridRecipesPresenter;
        private CookingMiniGamePresenter _cookingMiniGamePresenter;
        private CookingIngredientsPresenter _cookingIngredientsPresenter;

        public CookingPanelPresenter(IPanelView view, PresentersFactory presentersFactory) : base(view)
        {
            _view = view;
            _presentersFactory = presentersFactory;
        }

        protected override void OnShow()
        {
            SetupView();
            SetupLeftPanel();
            SetupMiniGame();
            SetupIngredients();
        }

        protected override void OnHide()
        {
            _view.OnCloseClicked -= Hide;
            
            _leftGridRecipesPresenter.Hide();
            _cookingMiniGamePresenter.Hide();
            _cookingIngredientsPresenter.Hide();
        }

        private void SetupView()
        {
            _view.SetTitle(Title);
            _view.OnCloseClicked += Hide;
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