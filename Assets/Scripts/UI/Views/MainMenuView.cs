using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class MainMenuView : MonoBehaviour, IView
    {
        [SerializeField] 
        private Button StartButton;
        
        [SerializeField]
        private Button QuitButton;

        private IMainMenuViewModel _viewModel;

        public void Show(IViewModel viewModel)
        {
            if (viewModel is not IMainMenuViewModel mainMenuViewModel)
            {
                throw new InvalidDataException($"{nameof(viewModel)} must be {nameof(IMainMenuViewModel)}");
            }

            _viewModel = mainMenuViewModel;
            StartButton.onClick.AddListener(OnStartButtonClicked);
            QuitButton.onClick.AddListener(OnQuitButtonClicked);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            StartButton.onClick.RemoveListener(OnStartButtonClicked);
            QuitButton.onClick.RemoveListener(OnQuitButtonClicked);
            gameObject.SetActive(false);
        }

        private void OnStartButtonClicked() => _viewModel.StartGame();

        private void OnQuitButtonClicked() => _viewModel.QuitGame();
    }
}