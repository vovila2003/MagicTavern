using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Tavern.UI
{
    public class Tester : MonoBehaviour
    {
        [SerializeField] 
        private Button Button;

        private PresentersFactory _factory;
        private LeftGridPresenter _presenter;

        [Inject]
        private void Construct(PresentersFactory factory)
        {
            _factory = factory;
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
            _presenter ??= _factory.CreateLeftGridPresenter();

            _presenter.Show();
        }
    }
}