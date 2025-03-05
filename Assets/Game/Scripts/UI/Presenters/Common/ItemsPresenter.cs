using System;
using System.Collections.Generic;
using Modules.Inventories;
using Modules.Items;
using Tavern.Utils;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public abstract class ItemsPresenter<T> : BasePresenter where T : Item
    {
        private readonly IContainerView _view;
        private readonly CommonPresentersFactory _commonPresentersFactory;
        private readonly IInventory<T> _inventory;
        private readonly Dictionary<Item, ItemCardPresenter> _presenters = new();
        private readonly Func<Transform, InfoPresenter> _infoPresenterFactory;
        private readonly Transform _canvas;
        
        private InfoPresenter _infoPresenter;

        protected string ActionName { get; set; }

        protected ItemsPresenter(
            IContainerView view,
            CommonPresentersFactory commonPresentersFactory,
            IInventory<T> inventory,
            Func<Transform, InfoPresenter> infoPresenterFactory,
            Transform canvas) : base(view)
        {
            _view = view;
            _commonPresentersFactory = commonPresentersFactory;
            _inventory = inventory;
            _infoPresenterFactory = infoPresenterFactory;
            _canvas = canvas;
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
            
            _inventory.OnItemAdded += Changed;
            _inventory.OnItemRemoved += Changed;
            _inventory.OnItemCountChanged += CountChanged;
        }

        protected override void OnHide()
        {
            ClearItems();
            
            _inventory.OnItemAdded -= Changed;
            _inventory.OnItemRemoved -= Changed;
            _inventory.OnItemCountChanged -= CountChanged;
        }

        private void SetupCards()
        {
            foreach (T item in _inventory.Items)
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

        private void OnLeftClick(Item item)
        {
            _infoPresenter ??= _infoPresenterFactory(_canvas);
            
            if (!_infoPresenter.Show(item, InfoPresenter.Mode.Dialog, ActionName)) return;
            
            _infoPresenter.OnAccepted += OnAction;
            _infoPresenter.OnRejected += OnCancelled;
        }

        protected abstract void OnRightClick(Item item);
        
        private void OnAction(Item item)
        {
            UnsubscribeInfo();
            OnRightClick(item);
        }

        private void OnCancelled() => UnsubscribeInfo();

        private void UnsubscribeInfo()
        {
            _infoPresenter.OnAccepted -= OnAction;
            _infoPresenter.OnRejected -= OnCancelled;
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