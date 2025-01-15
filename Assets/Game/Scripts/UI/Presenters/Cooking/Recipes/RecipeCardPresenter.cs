using JetBrains.Annotations;
using Modules.Items;
using Tavern.Cooking;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    [UsedImplicitly]
    public class RecipeCardPresenter
    {
        private readonly IEntityCardView _view;
        private readonly IEntityCardViewPool _pool;
        private bool _isShown;

        public RecipeCardPresenter(IEntityCardView view, IEntityCardViewPool pool)
        {
            _view = view;
            _pool = pool;
            _isShown = false;
        }

        public void Show(DishRecipe recipe)
        {
            if (_isShown) return;
            
            ItemMetadata metadata = recipe.ResultItem.Item.ItemMetadata;
            _view.SetTitle(metadata.Title);
            _view.SetIcon(metadata.Icon);
            _view.SetTime($"{recipe.CraftingTimeInSeconds} секунд");
            _view.SetStars(recipe.StarsCount);
            _view.OnCardClicked += OnClicked;
            
            _view.Show();
            _isShown = true;
        }

        public void Hide()
        {
            if (!_isShown) return;
            
            _view.OnCardClicked -= OnClicked;

            _view.Hide();
            _pool.UnspawnEntityCardView(_view);
            _isShown = false;
        }

        public void SetViewParent(Transform parent)
        {
            _view.SetParent(parent);
        }        

        private void OnClicked()
        {
            Debug.Log("Recipe clicked");
        }
    }
}