using Tavern.Shopping;

namespace Tavern.UI.Presenters
{
    public class CategoriesPresenter : BasePresenter
    {
        private readonly ICategoriesView _view;
        private SellerConfig _config;

        public CategoriesPresenter(ICategoriesView view) : base(view)
        {
            _view = view;
        }

        public void Show(SellerConfig config)
        {
            _config = config;
            Show();
        }

        protected override void OnShow()
        {
            SetupFilters();
        }

        protected override void OnHide()
        {
            
        }

        private void SetupFilters()
        {
            foreach (var VARIABLE in _config.Filter)
            {
                //TODO
            }
        }
    }
}