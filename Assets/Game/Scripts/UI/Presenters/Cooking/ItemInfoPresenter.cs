using System;
using Modules.Items;

namespace Tavern.UI.Presenters
{
    public sealed class ItemInfoPresenter : BasePresenter
    {
        public event Action<Item> OnAccepted;
        public event Action OnRejected;
        
        private readonly IInfoPanelView _view;
        private Item _item;
        private string _command;
        
        public ItemInfoPresenter(IInfoPanelView view) : base(view)
        {
            _view = view;
        }

        public void Show(Item item, string command)
        {
            _item = item;
            _command = command;
            Show();
        }

        protected override void OnShow()
        {
            if (_item == null) return;

            ItemMetadata metadata = _item.ItemMetadata;
            _view.SetTitle(metadata.Title);
            _view.SetDescription(metadata.Description);
            _view.SetIcon(metadata.Icon);
            _view.SetActionButtonText(_command);
            _view.OnAction += OnAction;
            _view.OnClose += OnClose;
        }

        protected override void OnHide()
        {
            _view.OnAction -= OnAction;
            _view.OnClose -= OnClose;
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
    }
}