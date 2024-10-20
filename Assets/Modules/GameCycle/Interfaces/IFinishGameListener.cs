namespace Tavern.Architecture.GameManager.Interfaces
{
    public interface IFinishGameListener : IGameListener
    {
        void OnFinish();
    }
}