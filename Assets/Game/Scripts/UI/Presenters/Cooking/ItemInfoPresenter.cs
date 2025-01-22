using System;
using Modules.Items;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public sealed class ItemInfoPresenter
    {
        public event Action<Item> OnAccepted;
        public event Action OnRejected;
        
        private readonly IInfoViewProvider _provider;
        private readonly Transform _parent;
        private Item _item;
        private IInfoPanelView _view;
        
        public ItemInfoPresenter(IInfoViewProvider provider, Transform parent)
        {
            _provider = provider;
            _parent = parent;
        }

        public bool Show(Item item, string command)
        {
            if (item == null) return false;
            
            if (!_provider.TryGetView(_parent, out IInfoPanelView view)) return false;
            
            _item = item;
            _view = view;
            SetupView(command);

            _view.Show();
            
            return true;
        }

        private void SetupView(string command)
        {
            ItemMetadata metadata = _item.ItemMetadata;
            _view.SetTitle(metadata.Title);
            _view.SetDescription(metadata.Description);
            _view.SetIcon(metadata.Icon);
            _view.SetActionButtonText(command);
            
            _view.OnAction += OnAction;
            _view.OnClose += OnClose;
        }

        private void OnAction()
        {
            OnAccepted?.Invoke(_item);
            Hide();
        }

        private void OnClose()
        {
            OnRejected?.Invoke();
            Hide();
        }

        private void Hide()
        {
            _view.OnAction -= OnAction;
            _view.OnClose -= OnClose;
            _view.Hide();
            
            _provider.TryRelease(_view);
        }
    }
}