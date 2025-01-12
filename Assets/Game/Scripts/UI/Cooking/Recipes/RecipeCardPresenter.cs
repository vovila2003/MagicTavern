using JetBrains.Annotations;
using Modules.Items;
using Tavern.Cooking;

namespace Tavern.UI
{
    [UsedImplicitly]
    public class RecipeCardPresenter
    {
        private readonly EntityCardView _view;

        public RecipeCardPresenter(EntityCardView view)
        {
            _view = view;
        }

        public void Show(DishRecipe recipe)
        {
            ItemMetadata metadata = recipe.ResultItem.Item.ItemMetadata;
            _view.SetTitle(metadata.Title);
            _view.SetIcon(metadata.Icon);
            _view.SetTime($"{recipe.CraftingTimeInSeconds} секунд");
            _view.SetStars(recipe.StarsCount);
            _view.OnCardClicked += OnClicked;
            
            _view.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _view.OnCardClicked -= OnClicked;
            
            _view.gameObject.SetActive(false);
        }

        private void OnClicked()
        {
            Hide();
        }
    }
}