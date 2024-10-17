namespace Tavern.Architecture.GameManager.Interfaces
{
    public interface ILateUpdateListener : IGameListener
    {
        void OnLateUpdate(float deltaTime);
    }
}