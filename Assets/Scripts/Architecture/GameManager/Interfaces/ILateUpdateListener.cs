namespace Architecture.Interfaces
{
    public interface ILateUpdateListener : IGameListener
    {
        void OnLateUpdate(float deltaTime);
    }
}