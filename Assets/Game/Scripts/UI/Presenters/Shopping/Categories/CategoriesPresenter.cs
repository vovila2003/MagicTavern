using System;
using System.Collections.Generic;
using Modules.Shopping;
using Tavern.Shopping;

namespace Tavern.UI.Presenters
{
    public class CategoriesPresenter : BasePresenter
    {
        public event Action OnShowAllGoods;
        public event Action<ComponentGroupConfig> OnShowGroup;
        public event Action OnShowBuyOut;
        
        private const string AllGoods = "Все товары";
        private readonly ICategoriesView _view;
        private readonly ShoppingPresentersFactory _factory;
        private readonly Dictionary<FilterPresenter, ComponentGroupConfig> _filterPresenters = new();
        private FilterPresenter _allGoodsPresenter;
        private readonly HashSet<ComponentGroupConfig> _filters = new();
        private FilterPresenter _currentFilter;

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
            
            _view.Left();
        }

        protected override void OnShow()
        {
            SetupAllGoods();
            SetupFilters();
            _view.OnBuyOut += OnBuyOut;
        }

        protected override void OnHide()
        {
            _view.OnBuyOut -= OnBuyOut;
            
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
                presenter.SetSelected(false);
                presenter.Show();
                _filterPresenters.Add(presenter, filter);
            }
        }

        private void SetupAllGoods()
        {
            _allGoodsPresenter ??= _factory.CreateFilterPresenter(_view.Container, AllGoods);
            _allGoodsPresenter.OnClicked += OnAllGoodsClicked;
            _allGoodsPresenter.SetSelected(true);
            _allGoodsPresenter.Show();
        }

        private void OnAllGoodsClicked(FilterPresenter _)
        {
            OnShowAllGoods?.Invoke();
            SetAllUnselected();
            _allGoodsPresenter.SetSelected(true);
        }

        private void OnFilterClicked(FilterPresenter filterPresenter)
        {
            if (!_filterPresenters.TryGetValue(filterPresenter, out ComponentGroupConfig config)) return;

            OnShowGroup?.Invoke(config);
            SetAllUnselected();
            filterPresenter.SetSelected(true);
        }

        private void SetAllUnselected()
        {
            foreach (FilterPresenter presenter in _filterPresenters.Keys)
            {
                presenter.SetSelected(false);
            }
            
            _allGoodsPresenter.SetSelected(false);
        }

        private void OnBuyOut()
        {
            SetAllUnselected();
            OnShowBuyOut?.Invoke();
        }
    }
}