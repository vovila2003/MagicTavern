namespace Tavern.Architecture.GameManager.Interfaces
{
    public interface IUpdateListener : IGameListener
    {
        void OnUpdate(float deltaTime);
    }
}