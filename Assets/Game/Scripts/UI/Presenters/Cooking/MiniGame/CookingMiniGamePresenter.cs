using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class CookingMiniGamePresenter : BasePresenter
    {
        private ICookingMiniGameView _view;
        
        public CookingMiniGamePresenter(ICookingMiniGameView view) : base(view)
        {
            _view = view;
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        public void MatchNewRecipe()
        {
            //TODO
            Debug.Log("Match new recipe");
        }
    }
}