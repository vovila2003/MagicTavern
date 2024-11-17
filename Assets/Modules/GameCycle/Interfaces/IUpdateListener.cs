namespace Modules.GameCycle.Interfaces
{
    public interface IUpdateListener : IGameListener
    {
        void OnUpdate(float deltaTime);
    }
}