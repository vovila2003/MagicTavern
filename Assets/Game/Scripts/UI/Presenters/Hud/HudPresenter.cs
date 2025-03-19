namespace Tavern.UI.Presenters
{
    public sealed class HudPresenter : BasePresenter
    {
        private readonly MinimapPresenter _minimapPresenter;
        
        public HudPresenter(
            IHudView view, 
            CommonPresentersFactory factory
            ) : base(view)
        {
            _minimapPresenter = factory.CreateMinimapPresenter(view.MiniMapView);
        }

        protected override void OnShow()
        {
            _minimapPresenter.Show();
        }

        protected override void OnHide()
        {
            _minimapPresenter.Hide();
        }
    }
}