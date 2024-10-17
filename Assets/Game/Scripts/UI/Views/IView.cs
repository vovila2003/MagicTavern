using Tavern.UI.ViewModels.Interfaces;

namespace Tavern.UI.Views
{
    public interface IView
    {
        void Show(IViewModel viewModel);

        void Hide();
    }
}