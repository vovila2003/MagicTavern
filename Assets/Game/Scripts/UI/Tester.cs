using Tavern.Cooking;
using Tavern.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Tavern.UI
{
    public class Tester : MonoBehaviour
    {
        [SerializeField] 
        private Button OpenButton;

        [SerializeField] 
        private RecipeType RecipeType;
        
        private CookingPresentersFactory _factory;
        private CookingPanelPresenter _presenter;
        private ActiveDishRecipe _recipe;

        [Inject]
        private void Construct(CookingPresentersFactory factory, ActiveDishRecipe recipe)
        {
            _factory = factory;
            _recipe = recipe;
        }

        private void OnEnable()
        {
            OpenButton.onClick.AddListener(OnOpen);
        }

        private void OnDisable()
        {
            OpenButton.onClick.RemoveListener(OnOpen);
        }

        private void OnOpen()
        {
            _recipe.Type = RecipeType;
            _presenter ??= _factory.CreateCookingPanelPresenter();

            _presenter.Show();
        }
    }
}