using Modules.Items;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class ItemCardPresenter
    {
        private readonly IItemCardView _view;
        private readonly IItemCardViewPool _pool;
        private Item _item;
        private bool _isShown;

        public ItemCardPresenter(IItemCardView view, IItemCardViewPool pool)
        {
            _view = view;
            _pool = pool;
            _isShown = false;
        }

        public void Show(Item item, int count)
        {
            if (_isShown) return;
            
            _item = item;
            
            SetupView(item, count);

            _view.Show();
            _isShown = true;
        }

        public void Hide()
        {
            if (!_isShown) return;
            
            _view.OnCardClicked -= OnClicked;

            _view.Hide();
            _pool.UnspawnItemCardView(_view);
            _isShown = false;
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