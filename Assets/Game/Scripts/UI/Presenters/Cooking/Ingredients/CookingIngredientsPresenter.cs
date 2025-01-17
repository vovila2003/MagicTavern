using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Tavern.Gardening;
using Tavern.Looting;

namespace Tavern.UI.Presenters
{
    public class CookingIngredientsPresenter
    {
        private readonly IContainerView _view;
        private readonly IInventory<ProductItem> _productInventory;
        private readonly IInventory<LootItem> _lootInventory;
        private readonly PresentersFactory _presentersFactory;
        private readonly Dictionary<Item, ItemCardPresenter> _presenters = new();
        private bool _isShown;

        public CookingIngredientsPresenter(
            IContainerView view,
            IInventory<ProductItem> productInventory,
            IInventory<LootItem> lootInventory,
            PresentersFactory presentersFactory
            )
        {
            _view = view;
            _productInventory = productInventory;
            _lootInventory = lootInventory;
            _presentersFactory = presentersFactory;
            _isShown = false;
        }

        public void Show()
        {
            if (_isShown) return;

            SetupCards();
            
            _productInventory.OnItemAdded += OnItemAdded;
            _productInventory.OnItemRemoved += OnItemRemoved;
            _lootInventory.OnItemAdded += OnItemAdded;
            _lootInventory.OnItemRemoved += OnItemRemoved;
            _view.Show();
            _isShown = true;
        }

        public void Hide()
        {
            if (!_isShown) return;

            _productInventory.OnItemAdded -= OnItemAdded;
            _productInventory.OnItemRemoved -= OnItemRemoved;
            _lootInventory.OnItemAdded -= OnItemAdded;
            _lootInventory.OnItemRemoved -= OnItemRemoved;

            _view.Hide();

            foreach (ItemCardPresenter presenter in _presenters.Values)
            {
                presenter.Hide();
            }
            
            _presenters.Clear();
            _isShown = false;
        }

        private void SetupCards()
        {
            foreach (ProductItem item in _productInventory.Items)
            {
                AddPresenter(item);
            }
            
            foreach (LootItem item in _lootInventory.Items)
            {
                AddPresenter(item);
            }
        }

        private void AddPresenter(Item item)
        {
            ItemCardPresenter presenter = _presentersFactory.CreateItemCardPresenter(_view.ContentTransform);
            _presenters.Add(item, presenter);
            presenter.Show(item);
        }

        private void OnItemAdded(Item item)
        {
            AddPresenter(item);
        }

        private void OnItemRemoved(Item item)
        {
            if (_presenters.Remove(item, out ItemCardPresenter presenter))
            {
                presenter.Hide();
            }
        }
    }
}