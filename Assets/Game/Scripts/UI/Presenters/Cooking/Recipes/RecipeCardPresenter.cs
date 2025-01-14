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

        public RecipeCardPresenter(IEntityCardView view)
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
            
            _view.Show();
        }

        public void DestroyView()
        {
            _view.OnCardClicked -= OnClicked;

            _view.Hide();
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