namespace Tavern.UI.Presenters
{
    public interface IEntityCardViewPool
    {
        bool TrySpawnEntityCardView(out IEntityCardView view);
        void UnspawnEntityCardView(IEntityCardView view);
    }
}