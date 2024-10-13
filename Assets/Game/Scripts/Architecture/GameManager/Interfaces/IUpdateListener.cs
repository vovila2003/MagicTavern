namespace Architecture.Interfaces
{
    public interface IUpdateListener : IGameListener
    {
        void OnUpdate(float deltaTime);
    }
}