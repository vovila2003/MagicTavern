using JetBrains.Annotations;
using Modules.Items;
using Tavern.Cooking;
using Tavern.UI.Views;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    [UsedImplicitly]
    public class RecipeCardPresenter
    {
        public EntityCardView View => _view;
        
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

        public void DestroyView()
        {
            _view.OnCardClicked -= OnClicked;

            Object.Destroy(_view.gameObject);
        }

        private void OnClicked()
        {
            Debug.Log("Recipe clicked");
        }
    }
}