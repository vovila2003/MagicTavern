namespace Tavern.UI.Presenters
{
    public interface IViewsFactory
    {
        ILeftGridView CreateLeftGridView();
        IEntityCardViewPool EntityCardViewPool { get; }
        IEntityCardView GetEntityCardView();
    }
}