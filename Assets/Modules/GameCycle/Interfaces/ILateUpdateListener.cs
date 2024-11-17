namespace Modules.GameCycle.Interfaces
{
    public interface ILateUpdateListener : IGameListener
    {
        void OnLateUpdate(float deltaTime);
    }
}