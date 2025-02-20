using System.Collections.Generic;
using Modules.Shopping;
using Tavern.Shopping;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class CategoriesPresenter : BasePresenter
    {
        private const string AllGoods = "Все товары";
        private readonly ICategoriesView _view;
        private SellerConfig _config;
        private readonly ShoppingPresentersFactory _factory;
        private Dictionary<FilterPresenter, ComponentGroupConfig> _filterPresenters = new();
        private FilterPresenter _allGoodsPresenter;

        public CategoriesPresenter(ICategoriesView view, ShoppingPresentersFactory factory) : base(view)
        {
            _view = view;
            _factory = factory;
        }

        public void Show(SellerConfig config)
        {
            _config = config;
            Show();
        }

        protected override void OnShow()
        {
            SetupAllGoods();
            SetupFilters();
        }

        protected override void OnHide()
        {
            _allGoodsPresenter.OnClicked -= OnAllGoodsClicked;
        }

        private void SetupFilters()
        {
            HashSet<ComponentGroupConfig> _configs = new();
            foreach (Preference preference in _config.Preferences)
            {
                //if (_configs.Add(preference.Group))
            }
        }

        private void SetupAllGoods()
        {
            _allGoodsPresenter ??= _factory.CreateFilterPresenter(_view.Container, AllGoods);
            _allGoodsPresenter.OnClicked += OnAllGoodsClicked;
            _allGoodsPresenter.Show();
        }

        private void OnAllGoodsClicked()
        {
            Debug.Log("All goods clicked");
        }
    }
}