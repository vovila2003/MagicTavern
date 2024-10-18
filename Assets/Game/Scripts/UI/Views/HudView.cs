using System.IO;
using Tavern.UI.ViewModels.Interfaces;
using UnityEngine;

namespace Tavern.UI.Views
{
    public sealed class HudView : MonoBehaviour, IView
    {
        private IHudViewModel _viewModel;

        public void Show(IViewModel viewModel)
        {
            if (viewModel is not IHudViewModel hudViewModel)
            {
                throw new InvalidDataException($"{nameof(viewModel)} must be {nameof(IHudViewModel)}");
            }

            _viewModel = hudViewModel;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}