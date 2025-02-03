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
        
        private CookingPresentersFactory _factory;
        private CookingPanelPresenter _presenter;

        [Inject]
        private void Construct(CookingPresentersFactory factory)
        {
            _factory = factory;
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
            _presenter ??= _factory.CreateCookingPanelPresenter();

            _presenter.Show();
        }
    }
}