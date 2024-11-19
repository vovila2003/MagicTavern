namespace Modules.GameCycle.Interfaces
{
    public interface IFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate(float fixedDeltaTime);
    }
}