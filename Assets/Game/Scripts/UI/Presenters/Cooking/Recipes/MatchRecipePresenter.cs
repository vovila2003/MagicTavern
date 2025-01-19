using System;

namespace Tavern.UI.Presenters
{
    public sealed class MatchRecipePresenter : BasePresenter
    {
        public event Action OnPressed;
        
        private readonly IMatchRecipeView _view;
        
        public MatchRecipePresenter(IMatchRecipeView view) : base(view)
        {
            _view = view;
        }

        protected override void OnShow()
        {
            _view.OnPressed += OnMatchRecipePressed;
        }

        protected override void OnHide()
        {
            _view.OnPressed -= OnMatchRecipePressed;
        }

        private void OnMatchRecipePressed()
        {
            OnPressed?.Invoke();
        }
    }
}