namespace Tavern.UI.Presenters
{
    public interface IViewsFactory
    {
        IEntityCardView CreateEntityCardView();
        ILeftGridView CreateLeftGridView();
    }
}