using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class PauseView : MonoBehaviour, IView
    {
        [SerializeField] 
        private Button ResumeButton;

        private IPauseViewModel _viewModel;

        public void Show(IViewModel viewModel)
        {
            if (viewModel is not IPauseViewModel pauseViewModel)
            {
                throw new InvalidDataException($"{nameof(viewModel)} must be {nameof(IPauseViewModel)}");
            }

            _viewModel = pauseViewModel;
            ResumeButton.onClick.AddListener(OnResumeButtonClicked);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            ResumeButton.onClick.RemoveListener(OnResumeButtonClicked);
            gameObject.SetActive(false);
        }

        private void OnResumeButtonClicked() => _viewModel.Resume();
    }
}