namespace Tavern.Architecture.GameManager.Interfaces
{
    public interface IPauseGameListener : IGameListener
    {
        void OnPause();
    }
}