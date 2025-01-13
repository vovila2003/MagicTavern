using JetBrains.Annotations;
using Tavern.Cooking;

namespace Tavern.UI
{
    [UsedImplicitly]
    public class PresentersFactory
    {
        private readonly ViewsFactory _viewsFactory;
        private readonly DishCookbookContext _dishCookbook;

        public PresentersFactory(ViewsFactory viewsViewsFactory, DishCookbookContext dishCookbook)
        {
            _viewsFactory = viewsViewsFactory;
            _dishCookbook = dishCookbook;
        }

        public RecipeCardPresenter CreateRecipeCardPresenter()
        {
            return new RecipeCardPresenter(_viewsFactory.CreateEntityCardView());
        }

        public LeftGridRecipesPresenter CreateLeftGridPresenter()
        {
            return new LeftGridRecipesPresenter(_viewsFactory.CreateLeftGridView(), this, _dishCookbook);
        }
    }
}