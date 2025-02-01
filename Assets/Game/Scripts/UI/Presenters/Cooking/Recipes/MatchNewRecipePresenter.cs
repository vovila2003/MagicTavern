using System;

namespace Tavern.UI.Presenters
{
    public sealed class MatchNewRecipePresenter : BasePresenter
    {
        public event Action OnPressed;
        
        private readonly IMatchNewRecipeView _view;
        
        public MatchNewRecipePresenter(IMatchNewRecipeView view) : base(view)
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