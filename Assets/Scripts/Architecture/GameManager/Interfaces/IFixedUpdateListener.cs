namespace Architecture.Interfaces
{
    public interface IFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate(float fixedDeltaTime);
    }
}