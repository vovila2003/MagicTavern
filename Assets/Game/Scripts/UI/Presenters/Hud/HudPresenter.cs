namespace Tavern.UI.Presenters
{
    public sealed class HudPresenter
    {
        private readonly IHudView _view;

        public HudPresenter(IHudView view)
        {
            _view = view;
        }

        public void Show()
        {
            _view.Show();
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}