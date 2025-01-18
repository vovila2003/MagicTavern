using Modules.Items;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class ItemCardPresenter : BasePresenter
    {
        private readonly IItemCardView _view;
        private readonly IItemCardViewPool _pool;
        private Item _item;
        private int _count;

        public ItemCardPresenter(IItemCardView view, IItemCardViewPool pool) : base(view)
        {
            _view = view;
            _pool = pool;
        }

        public void Show(Item item, int count)
        {
            _item = item;
            _count = count;
            Show();
        }

        protected override void OnShow()
        {
            SetupView(_item, _count);
        }

        protected override void OnHide()
        {
            _view.OnCardClicked -= OnClicked;
            _pool.UnspawnItemCardView(_view);
        }

        private void SetupView(Item item, int count)
        {
            ItemMetadata metadata = item.ItemMetadata;
            _view.SetIcon(metadata.Icon);
            _view.SetCount($"{count}");
            _view.OnCardClicked += OnClicked;
        }

        private void OnClicked()
        {
            Debug.Log($"Item {_item.ItemName} clicked");
        }
    }
}