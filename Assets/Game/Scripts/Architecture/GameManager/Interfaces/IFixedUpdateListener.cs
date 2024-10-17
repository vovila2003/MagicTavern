namespace Tavern.Architecture.GameManager.Interfaces
{
    public interface IFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate(float fixedDeltaTime);
    }
}