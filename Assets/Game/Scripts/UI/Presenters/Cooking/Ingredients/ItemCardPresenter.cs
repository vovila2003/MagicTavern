using System;
using Modules.Items;

namespace Tavern.UI.Presenters
{
    public class ItemCardPresenter : BasePresenter
    {
        public event Action<Item> OnRightClick;
        public event Action<Item> OnLeftClick;
        
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

        public void ChangeCount(int count)
        {
            _view.SetCount($"{count}");
        }

        protected override void OnShow()
        {
            ItemMetadata metadata = _item.ItemMetadata;
            _view.SetIcon(metadata.Icon);
            ChangeCount(_count);
            _view.OnLeftClicked += OnLeftClicked;
            _view.OnRightClicked += OnRightClicked;
        }

        protected override void OnHide()
        {
            _view.OnLeftClicked -= OnLeftClicked;
            _view.OnRightClicked -= OnRightClicked;
            _pool.UnspawnItemCardView(_view);
        }

        private void OnLeftClicked() => OnLeftClick?.Invoke(_item);

        private void OnRightClicked() => OnRightClick?.Invoke(_item);
    }
}