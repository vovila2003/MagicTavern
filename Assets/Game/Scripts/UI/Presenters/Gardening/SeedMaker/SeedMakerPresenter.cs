using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class SeedMakerPresenter : BasePresenter
    {
        private const string Title = "Создание семян";

        private readonly IPanelView _view;
        private readonly GardeningPresentersFactory _gardeningPresentersFactory;
        private SeedMakerProductItemsPresenter _productPresenter;

        public SeedMakerPresenter(
            IPanelView view,
            GardeningPresentersFactory gardeningPresentersFactory
            ) : base(view)
        {
            _view = view;
            _gardeningPresentersFactory = gardeningPresentersFactory;
        }
        
        protected override void OnShow()
        {
            SetupView();
            SetupProductItems();
        }

        protected override void OnHide()
        {
            _view.OnCloseClicked -= Hide;
            
            _productPresenter.Hide();
        }

        private void SetupView()
        {
            _view.SetTitle(Title);
            _view.OnCloseClicked += Hide;
        }

        private void SetupProductItems()
        {
            _productPresenter ??= _gardeningPresentersFactory.CreateSeedMakerProductItemsPresenter(_view.Container);
            _productPresenter.Show();
        }
    }
}