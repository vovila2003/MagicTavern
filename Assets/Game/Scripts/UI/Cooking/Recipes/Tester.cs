using Tavern.Cooking;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Tavern.UI
{
    public class Tester : MonoBehaviour
    {
        [SerializeField] 
        private Button Button;

        [SerializeField] 
        private Transform Parent;

        [SerializeField] 
        private DishRecipe Recipe;
        
        private EntityCardView _viewPrefab;
        private RecipeCardPresenter _presenter;

        [Inject]
        private void Construct(EntityCardView viewPrefab)
        {
            _viewPrefab = viewPrefab;
        }

        private void OnEnable()
        {
            Button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (Recipe == null)
            {
                Debug.Log("Recipe is null!");
                return; 
            }

            if (_presenter == null)
            {
                EntityCardView view = Instantiate(_viewPrefab, Parent);
                _presenter = new RecipeCardPresenter(view);
                
            }
            
            _presenter.Show(Recipe);
        }
    }
}