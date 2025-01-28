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
        private int _stars;

        public RecipeCardPresenter(
            IEntityCardView view, 
            IEntityCardViewPool pool) : base(view)
        {
            _view = view;
            _pool = pool;
        }

        public void Show(DishRecipe recipe, int recipeStars)
        {
            _recipe = recipe;
            _stars = recipeStars;
            Show();
        }

        public void SetStars(int value)
        {
            _stars = value;
            _view.SetStars(value / 2.0f);
        }

        protected override void OnShow()
        {
            SetupView(_recipe, _stars);
        }

        protected override void OnHide()
        {
            _view.OnCardClicked -= OnClicked;
            _pool.UnspawnEntityCardView(_view);
        }

        private void SetupView(DishRecipe recipe, int stars)
        {
            ItemMetadata metadata = recipe.ResultItem.GetItem().Metadata;
            _view.SetTitle(metadata.Title);
            _view.SetIcon(metadata.Icon);
            _view.SetTime($"{recipe.CraftingTimeInSeconds} секунд");
            _view.SetMaxStart(recipe.StarsCount);
            _view.SetStars(stars / 2.0f);
            _view.OnCardClicked += OnClicked;
        }

        private void OnClicked() => OnRecipeClicked?.Invoke(_recipe);
    }
}