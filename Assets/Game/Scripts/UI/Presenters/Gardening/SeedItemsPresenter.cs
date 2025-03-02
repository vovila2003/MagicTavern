using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Tavern.Gardening;
using Tavern.Utils;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class SeedItemsPresenter : BasePresenter
    {
        private IContainerView _view;
        private readonly CommonPresentersFactory _commonPresentersFactory;
        private readonly IInventory<SeedItem> _seedInventory;
        private readonly Dictionary<Item, ItemCardPresenter> _presenters = new();

        public SeedItemsPresenter(
            IContainerView view,
            CommonPresentersFactory commonPresentersFactory,
            IInventory<SeedItem> seedInventory
            ) : base(view)
        {
            _view = view;
            _commonPresentersFactory = commonPresentersFactory;
            _seedInventory = seedInventory;
        }

        protected override void OnShow()
        {
            SetupCards();
        }

        protected override void OnHide()
        {
            ClearItems();
        }

        private void SetupCards()
        {
            foreach (SeedItem item in _seedInventory.Items)
            {
                AddPresenter(item, item.GetCount());
            }
        }

        private void AddPresenter(SeedItem item, int itemCount)
        {
            if (itemCount <= 0) return;
            
            if (_presenters.TryGetValue(item, out ItemCardPresenter presenter))
            {
                presenter.ChangeCount(itemCount);
                return;
            }
            
            presenter = _commonPresentersFactory.CreateItemCardPresenter(_view.ContentTransform);
            _presenters.Add(item, presenter);
            presenter.OnRightClick += OnSeedRightClick;
            presenter.OnLeftClick += OnSeedLeftClick;
            presenter.Show(item, itemCount);
        }

        private void OnSeedLeftClick(Item item)
        {
            Debug.Log($"Left click to {item.ItemName}");
        }

        private void OnSeedRightClick(Item item)
        {
            Debug.Log($"Right click to {item.ItemName}");
            }

        private void ClearItems()
        {
            foreach (ItemCardPresenter presenter in _presenters.Values)
            {
                UnsubscribeItemCard(presenter);
                presenter.Hide();
            }

            _presenters.Clear();
        }

        private void UnsubscribeItemCard(ItemCardPresenter presenter)
        {
            presenter.OnRightClick -= OnSeedRightClick;
            presenter.OnLeftClick -= OnSeedLeftClick;
        }
    }
}