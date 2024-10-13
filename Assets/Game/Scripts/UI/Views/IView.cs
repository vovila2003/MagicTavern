namespace UI
{
    public interface IView
    {
        void Show(IViewModel viewModel);

        void Hide();
    }
}