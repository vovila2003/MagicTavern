using Tavern.UI.ViewModels.Interfaces;

namespace Tavern.UI
{
    public interface IViewModelFactory
    {
        IMainMenuViewModel CreateMainMenuViewModel();
        IPauseViewModel CreatePauseViewModel();
        IHudViewModel CreateHudViewModel();
    }
}