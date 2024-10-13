namespace UI
{
    public interface IViewModelFactory
    {
        IMainMenuViewModel CreateMainMenuViewModel();
        IPauseViewModel CreatePauseViewModel();
        IHudViewModel CreateHudViewModel();
    }
}