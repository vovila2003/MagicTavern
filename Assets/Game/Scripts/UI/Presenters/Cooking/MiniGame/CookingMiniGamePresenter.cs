using Tavern.Cooking;

namespace Tavern.UI.Presenters
{
    public class CookingMiniGamePresenter : BasePresenter
    {
        private readonly ICookingMiniGameView _view;
        private readonly DishCrafter _crafter;

        public CookingMiniGamePresenter(
            ICookingMiniGameView view,
            DishCrafter crafter) : base(view)
        {
            _view = view;
            _crafter = crafter;
        }
        
        protected override void OnShow()
        {
            _crafter.OnStateChanged += OnCraftStateChanged;
        }

        protected override void OnHide()
        {
            _crafter.OnStateChanged -= OnCraftStateChanged;
        }

        private void OnCraftStateChanged(bool state)
        {
            _view.SetStartButtonActive(state);
        }
    }
}