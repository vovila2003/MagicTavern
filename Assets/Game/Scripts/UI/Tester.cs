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
        private Button CloseButton;

        private PresentersFactory _factory;
        private LeftGridRecipesPresenter _recipesPresenter;

        [Inject]
        private void Construct(PresentersFactory factory)
        {
            _factory = factory;
        }

        private void OnEnable()
        {
            OpenButton.onClick.AddListener(OnOpen);
            CloseButton.onClick.AddListener(OnClose);
        }

        private void OnDisable()
        {
            OpenButton.onClick.RemoveListener(OnOpen);
            CloseButton.onClick.RemoveListener(OnClose);
        }

        private void OnOpen()
        {
            _recipesPresenter ??= _factory.CreateLeftGridPresenter();

            _recipesPresenter.Show();
        }

        private void OnClose()
        {
            _recipesPresenter.Hide();
        }
    }
}