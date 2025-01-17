using Modules.Items;
using Tavern.Cooking;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class RecipeCardPresenter
    {
        private readonly IEntityCardView _view;
        private readonly IEntityCardViewPool _pool;
        private DishRecipe _recipe;
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
            
            _recipe = recipe;
            
            SetupView(recipe);

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

        private void SetupView(DishRecipe recipe)
        {
            ItemMetadata metadata = recipe.ResultItem.Item.ItemMetadata;
            _view.SetTitle(metadata.Title);
            _view.SetIcon(metadata.Icon);
            _view.SetTime($"{recipe.CraftingTimeInSeconds} секунд");
            _view.SetStars(recipe.StarsCount);
            _view.OnCardClicked += OnClicked;
        }

        private void OnClicked()
        {
            Debug.Log($"Recipe {_recipe.Name} clicked");
        }
    }
}