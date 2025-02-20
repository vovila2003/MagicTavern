using System;

namespace Tavern.UI.Presenters
{
    public class FilterPresenter : BasePresenter
    {
        public event Action OnClicked; 
        
        private readonly IFilterView _view;
        private readonly string _text;

        public FilterPresenter(IFilterView view, string text) : base(view)
        {
            _view = view;
            _text = text;
        }
        
        protected override void OnShow()
        {
            _view.SetText(_text);
            _view.OnFilterClicked += OnFilterClicked;
        }

        protected override void OnHide()
        {
            _view.OnFilterClicked -= OnFilterClicked;
        }

        private void OnFilterClicked()
        {
            OnClicked?.Invoke();
        }
    }
}