namespace Modules.Timers
{
    public interface ITickable
    {
        void Tick(float deltaTime);
    }
}