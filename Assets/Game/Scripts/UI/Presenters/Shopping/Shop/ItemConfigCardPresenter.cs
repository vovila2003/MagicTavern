using System;
using Modules.Info;
using Modules.Items;

namespace Tavern.UI.Presenters
{
    public class ItemConfigCardPresenter : BasePresenter
    {
        public event Action<ItemConfig> OnRightClick;
        public event Action<ItemConfig> OnLeftClick;
        
        private readonly IItemCardView _view;
        private readonly IItemCardViewPool _pool;
        private ItemConfig _itemConfig;
        private int _count;
        private int _price;

        public ItemConfigCardPresenter(IItemCardView view, IItemCardViewPool pool) : base(view)
        {
            _view = view;
            _pool = pool;
        }

        public void Show(ItemConfig item, int count, int price)
        {
            _itemConfig = item;
            _count = count;
            _price = price;
            Show();
        }

        public void ChangeCount(int count)
        {
            _view.SetCount($"{count}");
        }

        protected override void OnShow()
        {
            Metadata metadata = _itemConfig.Metadata;
            _view.SetIcon(metadata.Icon);
            ChangeCount(_count);
            _view.SetPriceActive(true);
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

        private void OnLeftClicked() => OnLeftClick?.Invoke(_itemConfig);

        private void OnRightClicked() => OnRightClick?.Invoke(_itemConfig);
    }
}