using Tavern.UI.Views;

namespace Tavern.UI.Presenters
{
    public sealed class HudPresenter
    {
        private readonly HudView _view;

        public HudPresenter(HudView view)
        {
            _view = view;
        }

        public void Show()
        {
            _view.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _view.gameObject.SetActive(false);
        }
    }
}