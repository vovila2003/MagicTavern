namespace Tavern.Architecture.GameManager.Interfaces
{
    public interface IResumeGameListener : IGameListener
    {
        void OnResume();
    }
}