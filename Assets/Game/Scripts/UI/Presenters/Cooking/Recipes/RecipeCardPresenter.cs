using System;
using Modules.Info;
using Tavern.Cooking;

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

        public void SetSelected(bool selected)
        {
            _view.SetSelected(selected);
        }

        public void Up(int position)
        {
            _view.Transform.SetSiblingIndex(position);
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
            Metadata metadata = recipe.ResultItemConfig.Metadata;
            _view.SetTitle(metadata.Title);
            _view.SetIcon(metadata.Icon);
            _view.SetTime($"{recipe.CraftingTimeInSeconds} секунд");
            _view.SetMaxStars(recipe.StarsCount);
            _view.SetStars(stars / 2.0f);
            _view.OnCardClicked += OnClicked;
        }

        private void OnClicked() => OnRecipeClicked?.Invoke(_recipe);
    }
}