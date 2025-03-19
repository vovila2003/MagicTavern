namespace Tavern.UI.Presenters
{
    public interface IHudView : IView
    {
        IMiniMapView MiniMapView { get; }
    }
}