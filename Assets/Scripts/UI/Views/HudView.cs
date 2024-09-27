using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class HudView : MonoBehaviour, IView
    {
        [SerializeField] 
        private Button PauseButton;

        private IHudViewModel _viewModel;

        public void Show(IViewModel viewModel)
        {
            if (viewModel is not IHudViewModel hudViewModel)
            {
                throw new InvalidDataException($"{nameof(viewModel)} must be {nameof(IHudViewModel)}");
            }

            _viewModel = hudViewModel;
            PauseButton.onClick.AddListener(OnPauseButtonClicked);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            PauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            gameObject.SetActive(false);
        }

        private void OnPauseButtonClicked() => _viewModel.Pause();
    }
}