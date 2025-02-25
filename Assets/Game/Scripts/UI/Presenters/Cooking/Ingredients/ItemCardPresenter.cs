using System;
using Modules.Info;
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
        private bool _showPrice;
        private int _price;

        public ItemCardPresenter(IItemCardView view, IItemCardViewPool pool) : base(view)
        {
            _view = view;
            _pool = pool;
        }

        public void Show(Item item, int count = 1, bool showPrice = false, int price = 0)
        {
            _item = item;
            _count = count;
            _showPrice = showPrice;
            _price = price;
            Show();
        }

        public void ChangeCount(int count)
        {
            _view.SetCount($"{count}");
        }

        protected override void OnShow()
        {
            Metadata metadata = _item.Metadata;
            _view.SetIcon(metadata.Icon);
            ChangeCount(_count);
            _view.SetPriceActive(_showPrice);
            _view.SetPrice($"{_price}");
            
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