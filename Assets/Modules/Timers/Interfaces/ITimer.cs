namespace Modules.Timers.Interfaces
{
    public interface ITimer :
        IStartable,
        IStoppable,
        IPlayable,
        IPausable,
        IResumable,
        IProgressable,
        IEndable,
        ITickable
    {
    }
}

