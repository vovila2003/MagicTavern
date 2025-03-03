using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Tavern.Utils;

namespace Tavern.UI.Presenters
{
    public abstract class ItemsPresenter<T> : BasePresenter where T : Item
    {
        private readonly IContainerView _view;
        private readonly CommonPresentersFactory _commonPresentersFactory;
        protected readonly IInventory<T> Inventory;
        private readonly Dictionary<Item, ItemCardPresenter> _presenters = new();

        protected ItemsPresenter(
            IContainerView view,
            CommonPresentersFactory commonPresentersFactory,
            IInventory<T> inventory) : base(view)
        {
            _view = view;
            _commonPresentersFactory = commonPresentersFactory;
            Inventory = inventory;
        }

        public void SetActive(bool active)
        {
            foreach (ItemCardPresenter presenter in _presenters.Values)
            {
                presenter.SetActive(active);
            }
        }

        protected override void OnShow()
        {
            SetupCards();
            
            Inventory.OnItemAdded += Changed;
            Inventory.OnItemRemoved += Changed;
            Inventory.OnItemCountChanged += CountChanged;
        }

        protected override void OnHide()
        {
            ClearItems();
            
            Inventory.OnItemAdded -= Changed;
            Inventory.OnItemRemoved -= Changed;
            Inventory.OnItemCountChanged -= CountChanged;
        }

        private void SetupCards()
        {
            foreach (T item in Inventory.Items)
            {
                AddPresenter(item, item.GetCount());
            }
        }

        private void AddPresenter(T item, int itemCount)
        {
            if (itemCount <= 0) return;
            
            if (_presenters.TryGetValue(item, out ItemCardPresenter presenter))
            {
                presenter.ChangeCount(itemCount);
                return;
            }
            
            presenter = _commonPresentersFactory.CreateItemCardPresenter(_view.ContentTransform);
            _presenters.Add(item, presenter);
            presenter.OnRightClick += OnRightClick;
            presenter.OnLeftClick += OnLeftClick;
            presenter.Show(item, itemCount);
        }

        protected abstract void OnLeftClick(Item item);
        protected abstract void OnRightClick(Item item);

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
            presenter.OnRightClick -= OnRightClick;
            presenter.OnLeftClick -= OnLeftClick;
        }

        private void Changed(Item item, IInventoryBase inventory)
        {
            ClearItems();
            SetupCards();
        }

        private void CountChanged(Item item, int count)
        {
            if (!_presenters.TryGetValue(item, out ItemCardPresenter presenter)) return;
            
            presenter.ChangeCount(count);
        }
    }
}