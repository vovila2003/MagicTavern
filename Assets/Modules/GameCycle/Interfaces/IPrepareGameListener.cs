namespace Tavern.Architecture.GameManager.Interfaces
{
    public interface IPrepareGameListener : IGameListener
    {
        void OnPrepare();
    }
}