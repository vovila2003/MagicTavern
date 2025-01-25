using System;
using Modules.Items;
using Tavern.Cooking;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class RecipeCardPresenter : BasePresenter
    {
        public event Action<DishRecipe> OnRecipeClicked;
        
        private readonly IEntityCardView _view;
        private readonly IEntityCardViewPool _pool;
        private DishRecipe _recipe;

        public RecipeCardPresenter(
            IEntityCardView view, 
            IEntityCardViewPool pool) : base(view)
        {
            _view = view;
            _pool = pool;
        }

        public void Show(DishRecipe recipe)
        {
            _recipe = recipe;
            Show();
        }

        protected override void OnShow()
        {
            SetupView(_recipe);
        }

        protected override void OnHide()
        {
            _view.OnCardClicked -= OnClicked;
            _pool.UnspawnEntityCardView(_view);
        }

        private void SetupView(DishRecipe recipe)
        {
            ItemMetadata metadata = recipe.ResultItem.Item.ItemMetadata;
            _view.SetTitle(metadata.Title);
            _view.SetIcon(metadata.Icon);
            _view.SetTime($"{recipe.CraftingTimeInSeconds} секунд");
            _view.SetMaxStart(recipe.StarsCount);
            //TODO
            _view.SetStars(0);
            _view.OnCardClicked += OnClicked;
        }

        private void OnClicked() => OnRecipeClicked?.Invoke(_recipe);
    }
}