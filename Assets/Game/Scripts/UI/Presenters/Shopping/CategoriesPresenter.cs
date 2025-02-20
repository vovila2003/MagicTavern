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
        private readonly ShoppingPresentersFactory _factory;
        private readonly Dictionary<FilterPresenter, ComponentGroupConfig> _filterPresenters = new();
        private FilterPresenter _allGoodsPresenter;
        private readonly HashSet<ComponentGroupConfig> _filters = new();

        public CategoriesPresenter(ICategoriesView view, ShoppingPresentersFactory factory) : base(view)
        {
            _view = view;
            _factory = factory;
        }

        public void Show(IReadOnlyCollection<ItemInfoByConfig> items)
        {
            foreach (ItemInfoByConfig info in items)
            {
                if (!info.Item.TryGet(out ComponentGroup componentGroup)) continue;
                _filters.Add(componentGroup.Config);
            }
            
            Show();
        }

        protected override void OnShow()
        {
            SetupAllGoods();
            SetupFilters();
        }

        protected override void OnHide()
        {
            _allGoodsPresenter.Hide();
            _allGoodsPresenter.OnClicked -= OnAllGoodsClicked;

            foreach (FilterPresenter presenter in _filterPresenters.Keys)
            {
                presenter.Hide();
                presenter.OnClicked -= OnFilterClicked;
            }
            _filterPresenters.Clear();
        }

        private void SetupFilters()
        {
            foreach (ComponentGroupConfig filter in _filters)
            {
                FilterPresenter presenter = _factory.CreateFilterPresenter(_view.Container, filter.Name);
                presenter.OnClicked += OnFilterClicked;
                presenter.Show();
                _filterPresenters.Add(presenter, filter);
            }
        }

        private void SetupAllGoods()
        {
            _allGoodsPresenter ??= _factory.CreateFilterPresenter(_view.Container, AllGoods);
            _allGoodsPresenter.OnClicked += OnAllGoodsClicked;
            _allGoodsPresenter.Show();
        }

        private void OnAllGoodsClicked(FilterPresenter _)
        {
            Debug.Log("All goods clicked");
        }

        private void OnFilterClicked(FilterPresenter filterPresenter)
        {
            if (!_filterPresenters.TryGetValue(filterPresenter, out ComponentGroupConfig config)) return;

            Debug.Log($"{config.Name} clicked");
        }
    }
}